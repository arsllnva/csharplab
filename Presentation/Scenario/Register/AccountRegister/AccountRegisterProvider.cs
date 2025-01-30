using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.Register.AccountRegister;

public class AccountRegisterProvider : IScenarioProvider
{
    private readonly IAccountService _service;
    private readonly ICurrentAccount _current;

    public AccountRegisterProvider(
        IAccountService service,
        ICurrentAccount currentAcc)
    {
        _service = service;
        _current = currentAcc;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_current.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new AccountRegisterScenario(_service);
        return true;
    }
}