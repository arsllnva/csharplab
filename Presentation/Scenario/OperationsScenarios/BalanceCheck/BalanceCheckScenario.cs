using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.OperationsScenarios.BalanceCheck;

public class BalanceCheckScenario : IScenario
{
    private readonly IUserService _userService;

    public BalanceCheckScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Check balance";

    public void Run()
    {
        int id = AnsiConsole.Ask<int>("What's your id?");
        BalanceResult result = _userService.GetBalance(id);

        AnsiConsole.WriteLine($"Current account balance: {result}\n");
        AnsiConsole.Ask<string>("Done");
    }
}