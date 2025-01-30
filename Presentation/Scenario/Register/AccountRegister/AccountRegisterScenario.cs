using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.Register.AccountRegister;

public class AccountRegisterScenario : IScenario
{
    private readonly IAccountService _accService;

    public AccountRegisterScenario(IAccountService accService)
    {
        _accService = accService;
    }

    public string Name => "Register account";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your username");
        int number = AnsiConsole.Ask<int>("Enter your number");
        string password = AnsiConsole.Ask<string>("Enter your password");
        double balance = AnsiConsole.Ask<double>("Enter your balance");

        CreateAccountResult result = _accService.CreateAccount(username, number, password, balance);

        string message = result switch
        {
            CreateAccountResult.Success => "Successfully  logged in",
            CreateAccountResult.Failure(" ") => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Done successfully");
    }
}