using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.OperationsScenarios.Withdraw;

public class WithdrawScenario : IScenario
{
    private readonly IUserService _userService;

    public WithdrawScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Withdraw";

    public void Run()
    {
        double amount = AnsiConsole.Ask<double>("How much would you like to withdraw?");
        OperationResult result = _userService.Withdraw(amount);

        string message = result switch
        {
            OperationResult.Success => "Successfully  withdrew",
            OperationResult.Failure("?") => "Smth is wrong",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Done successfully");
    }
}