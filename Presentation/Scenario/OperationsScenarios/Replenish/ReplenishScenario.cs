using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.OperationsScenarios.Replenish;

public class ReplenishScenario : IScenario
{
    private readonly IUserService _userService;

    public ReplenishScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "REplenish";

    public void Run()
    {
        double amount = AnsiConsole.Ask<double>("How much would you like to withdraw?");

        OperationResult result = _userService.Replenish(amount);

        string message = result switch
        {
            OperationResult.Success => "Successfully  logged in",
            OperationResult.Failure("User not found") => "User not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Done successfully");
    }
}