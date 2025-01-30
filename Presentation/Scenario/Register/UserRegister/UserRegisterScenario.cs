using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.Register.UserRegister;

public class UserRegisterScenario : IScenario
{
    private readonly IUserService _userService;

    public UserRegisterScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Register user";

    public void Run()
    {
        UserRole role = AnsiConsole.Ask<UserRole>("Enter your role");
        string username = AnsiConsole.Ask<string>("Enter your username");
        string password = AnsiConsole.Ask<string>("Enter your password");

        CreateUserResult result = _userService.Register(role, username, password);

        string message = result switch
        {
            CreateUserResult.Success => "Successfully  registered",
            CreateUserResult.Failure(" ") => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Done successfully");
    }
}