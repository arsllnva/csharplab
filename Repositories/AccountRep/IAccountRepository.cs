using Itmo.ObjectOrientedProgramming.Lab5.Models.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Repositories.AccountRep;

public interface IAccountRepository
{
    Account? FindAccount(int number);

    void AddAccount(string username, int number, string password, double balance);

    BalanceResult GetBalance(int id);

    void Withdraw(int id, double amount);

    void Replenish(int id, double amount);
}