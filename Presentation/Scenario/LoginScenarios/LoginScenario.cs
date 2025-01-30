using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.LoginScenarios;

public class LoginScenario : IScenario
{
    private readonly IUserService _userService;

    public LoginScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Login";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        string password = AnsiConsole.Ask<string>("Enter your password");

        LoginResult result = _userService.Login(username, password);

        string message = result switch
        {
            LoginResult.Success => "Successfully  logged in",
            LoginResult.Failure(" ") => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Done successfully");
    }
}