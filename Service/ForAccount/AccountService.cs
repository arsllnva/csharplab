using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.AccountRep;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.OperationRep;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Service.ForUser;

namespace Itmo.ObjectOrientedProgramming.Lab5.Service.ForAccount;

public class AccountService : IAccountService
{
    private readonly CurrentUser _currentUser;
    private readonly CurrentAccount _currentAccount;
    private readonly IAccountRepository _accountRepository;
    private readonly IOperationRepository _operationRepository;

    public AccountService(
        CurrentUser currentUser,
        CurrentAccount currentAccount,
        IAccountRepository accountRepository,
        IOperationRepository operationRepository)
    {
        _currentUser = currentUser;
        _currentAccount = currentAccount;
        _accountRepository = accountRepository;
        _operationRepository = operationRepository;
    }

    public CreateAccountResult CreateAccount(string username, int number, string pin, double balance)
    {
        User? user = _currentUser.User;
        if (user != null && username != user.Username)
        {
            return new CreateAccountResult.Failure("Username does not match current user");
        }

        if (user == null)
        {
            return new CreateAccountResult.Failure("User is nonexistent");
        }

        Account? existingAccount = _accountRepository.FindAccount(number);
        if (existingAccount != null)
        {
            return new CreateAccountResult.Failure("Account with this number already exists");
        }

        _accountRepository.AddAccount(user.Username, number, pin, balance);
        return new CreateAccountResult.Success(user.Id);
    }

    public LoginResult Login(int number, string pin)
    {
        Account? account = _accountRepository.FindAccount(number);
        if (account == null)
        {
            return new LoginResult.Failure("Account not found");
        }

        if (account.Pin != pin)
        {
            return new LoginResult.Failure("Invalid password");
        }

        _currentAccount.Account = account;

        return new LoginResult.Success();
    }

    public BalanceResult GetBalance(int number)
    {
        Account? account = _accountRepository.FindAccount(number);
        if (account == null)
        {
            return new BalanceResult.Failure("Account is not logged in");
        }

        return new BalanceResult.Success(account.Balance);
    }

    public OperationResult Withdraw(double amount)
    {
        User? user = _currentUser.User;
        Account? account = _currentAccount.Account;
        if (account == null)
        {
            return new OperationResult.Failure("Account is not logged in");
        }

        if (amount <= 0)
        {
            return new OperationResult.Failure("Withdrawal amount must be greater than zero");
        }

        if (account.Balance < amount)
        {
            return new OperationResult.Failure("Insufficient funds");
        }

        account = account with { Balance = account.Balance - amount };

        _accountRepository.Withdraw(account.Number, amount);
        _operationRepository.AddOperation(user, new Operation(amount, TypeOfOperation.Withdrawal));

        return new OperationResult.Success();
    }

    public OperationResult Replenish(double amount)
    {
        User? user = _currentUser.User;
        Account? account = _currentAccount.Account;
        if (account == null)
        {
            return new OperationResult.Failure("Account is not logged in");
        }

        if (amount <= 0)
        {
            return new OperationResult.Failure("Replenishment amount must be greater than zero");
        }

        account = account with { Balance = account.Balance + amount };

        _accountRepository.Replenish(account.Number, amount);
        _operationRepository.AddOperation(user, new Operation(amount, TypeOfOperation.Replenishment));

        return new OperationResult.Success();
    }
}