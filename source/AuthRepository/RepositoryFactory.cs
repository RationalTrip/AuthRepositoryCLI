using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthRepository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public ILoginAuthRepository GetLoginAuthRepository(string connectionString)
        {
            var passwordHasher = GetPasswordHasher();
            var saltGenerator = GetSaltGenerator();

            return new LoginAuthRepository(connectionString, saltGenerator, passwordHasher);
        }

        public IPasswordHasher GetPasswordHasher() => new PasswordHasher();

        public ISaltGenerator GetSaltGenerator() => new SaltGenerator();
    }
}
