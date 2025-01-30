using Itmo.ObjectOrientedProgramming.Lab5.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForAccounts;

public interface ICurrentAccount
{
    Account? Account { get; }
}