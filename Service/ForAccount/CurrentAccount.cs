using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Accounts;

namespace Itmo.ObjectOrientedProgramming.Lab5.Service.ForAccount;

public class CurrentAccount : ICurrentAccount
{
    public Account? Account { get; set; }
}