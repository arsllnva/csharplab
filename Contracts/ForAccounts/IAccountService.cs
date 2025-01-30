using Itmo.ObjectOrientedProgramming.Lab5.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForAccounts;

public interface IAccountService
{
    CreateAccountResult CreateAccount(string username, int number, string pin, double balance);

    LoginResult Login(int number, string pin);

    BalanceResult GetBalance(int number);

    OperationResult Withdraw(double amount);

    OperationResult Replenish(double amount);
}