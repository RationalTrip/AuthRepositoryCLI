using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AuthRepository
{
    public class LoginAuthRepository:ILoginAuthRepository
    {
        readonly ISaltGenerator _saltGenerator;
        readonly IPasswordHasher _passwordHasher;
        readonly string _connectionString;

        public LoginAuthRepository(string connectionString, ISaltGenerator saltGenerator,
            IPasswordHasher passwordHasher)
        {
            _connectionString = connectionString;
            _saltGenerator = saltGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginAuth> CreateUserAsync(LoginAuth userAuth)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var isLoginExist = await IsLoginExistsAsync(userAuth.Login, db);

            if (isLoginExist)
                return null;

            string salt = _saltGenerator.GenerateSalt();
            userAuth.SetSalt(salt);

            string password = _passwordHasher.HashPassword(salt, userAuth.Password);
            userAuth.SetPassword(password);

            var insertedCount = await db.ExecuteAsync(SqlQueries.InsertLoginAuthQuery,
                new { login = userAuth.Login, password = userAuth.Password, salt = userAuth.Salt });

            if (insertedCount < 1)
                return null;

            return userAuth;
        }

        public async Task<LoginAuth> SignInUserAsync(LoginAuth userToAuth)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var trueUserAuth = await db.QuerySingleOrDefaultAsync<LoginAuth>(SqlQueries.SelectUserByLogin,
                new { login = userToAuth.Login });

            if (trueUserAuth is null)
                return null;

            string salt = trueUserAuth.Salt;
            string passwordHashed = _passwordHasher.HashPassword(salt, userToAuth.Password);

            if (passwordHashed != trueUserAuth.Password)
                return null;

            return trueUserAuth;
        }

        public async Task<bool> IsLoginExistsAsync(string login)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            return await IsLoginExistsAsync(login, db);
        }

        private async Task<bool> IsLoginExistsAsync(string login, IDbConnection db)
        {
            var usersCount = await db.QuerySingleOrDefaultAsync<int>(SqlQueries.IsLoginAuthExistQuery,
                new { login = login });

            return usersCount > 0;
        }
    }
}
