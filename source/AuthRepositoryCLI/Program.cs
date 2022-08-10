using AuthRepository;
using AuthRepositoryCLI;
using Microsoft.Extensions.Configuration;



var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsetting.json")
    .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

var repoFactory = new RepositoryFactory();

var authService = new ConsoleAuthService(repoFactory.GetLoginAuthRepository(connectionString));

var isLoginExistCommand = "check";
var signInCommand = "signin";
var createCommand = "create";
var exitCommand = "exit";

while (true)
{
    Console.WriteLine("Enter command:");

    Console.WriteLine($"\t\"{isLoginExistCommand}\" to check is current login already exists.");
    Console.WriteLine($"\t\"{signInCommand}\" to sign in.");
    Console.WriteLine($"\t\"{createCommand}\" to create new auth.");
    Console.WriteLine($"\t\"{exitCommand}\" to exit programm");

    var command = Console.ReadLine().ToLower();

    if(command == isLoginExistCommand)
    {
        await authService.IsLoginExistsAsync();
        continue;
    }

    if (command == signInCommand)
    {
        await authService.SignInAsync();
        continue;
    }

    if (command == createCommand)
    {
        await authService.CreateAsync();
        continue;
    }

    if (command == exitCommand)
        break;

    Console.WriteLine("You enter wrong command, please try again.");
}

