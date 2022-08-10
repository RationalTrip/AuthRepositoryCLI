using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthRepository;

namespace AuthRepositoryCLI
{
    internal class ConsoleAuthService
    {
        readonly ILoginAuthRepository _repository;
        public ConsoleAuthService(ILoginAuthRepository repository) => _repository = repository;

        public async Task SignInAsync()
        {
            string login = EnterLogin();

            if (string.IsNullOrWhiteSpace(login))
                return;

            string password = EnterPassword();

            if (string.IsNullOrWhiteSpace(password))
                return;

            var auth = await _repository.SignInUserAsync(new LoginAuth(login, password));

            if(auth is not null)
            {
                Console.WriteLine("Sign in was successfull!");
                Console.WriteLine();

                PrintLoginAuth(auth);
            }
            else
            {
                Console.WriteLine("Sign in failed!");
            }
        }

        public async Task CreateAsync()
        {
            string login = EnterLogin();

            if (string.IsNullOrWhiteSpace(login))
                return;

            string password = EnterPassword();

            if (string.IsNullOrWhiteSpace(password))
                return;

            var auth = await _repository.CreateUserAsync(new LoginAuth(login, password));

            if (auth is not null)
            {
                Console.WriteLine("Creation was successfull!");
                Console.WriteLine();

                PrintLoginAuth(auth);
            }
            else
            {
                Console.WriteLine($"Creation failed! Auth with Login = \"{login}\" may already exists!");
            }
        }

        public async Task IsLoginExistsAsync()
        {
            string login = EnterLogin();

            if (string.IsNullOrWhiteSpace(login))
                return;

            var isLoginExists = await _repository.IsLoginExistsAsync(login);

            if (isLoginExists)
            {
                Console.WriteLine($"Auth with Login = \"{login}\" already exists!");
            }
            else
            {
                Console.WriteLine($"Auth with Login = \"{login}\" not exists!");
            }
        }

        static void PrintLoginAuth(LoginAuth auth)
        {
            Console.WriteLine("Login auth");

            Console.WriteLine($"\tId:\t{auth.AuthId}");
            Console.WriteLine($"\tLogin:\t{auth.Login}");
            Console.WriteLine($"\tHashed Password:\t{auth.Password}");
            Console.WriteLine($"\tSalt:\t{auth.Salt}");
        }
        static string EnterLogin() => EnterValue("Login");
        static string EnterPassword() => EnterValue("Password");
        static string EnterValue(string valueName)
        {
            Console.Write($"Enter {valueName}: ");
            string value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"{valueName} must not be empty.");

                return null;
            }

            return value;
        }
    }
}
