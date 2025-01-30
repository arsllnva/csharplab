using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.OperationsScenarios.History;

public class HistoryCheckProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUser;

    public HistoryCheckProvider(
        IUserService service,
        ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new HistoryCheckScenario(_service);
        return true;
    }
}