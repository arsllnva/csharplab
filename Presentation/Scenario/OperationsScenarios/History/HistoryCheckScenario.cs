using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.OperationsScenarios.History;

public class HistoryCheckScenario : IScenario
{
    private readonly IUserService _userService;

    public HistoryCheckScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Check history";

    public void Run()
    {
        OperationResult result = _userService.GetHistory();

        string message = result switch
        {
            OperationResult.Success => "Success",
            OperationResult.Failure("?") => "Failed to check history",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Done successfully");
    }
}