using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.AccountRep;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.OperationRep;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.UserRep;
using Itmo.ObjectOrientedProgramming.Lab5.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Service.ForUser;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly CurrentUser _currentUser;

    public UserService(
        IUserRepository userRepository,
        IOperationRepository operationRepository,
        IAccountRepository accountRepository,
        CurrentUser currentUser)
    {
        _userRepository = userRepository;
        _operationRepository = operationRepository;
        _accountRepository = accountRepository;
        _currentUser = currentUser;
    }

    public LoginResult Login(string username, string password)
    {
        User? user = _userRepository.FindUserByName(username);

        if (user == null)
            return new LoginResult.Failure("User not found.");

        if (user.Role == UserRole.Admin)
        {
            if (user.SystemPassword != password)
                return new LoginResult.Failure("Incorrect password for Admin.");
        }
        else
        {
            Account? account = _accountRepository.FindAccount(user.Id);
            if (account == null || account.Pin != password)
                return new LoginResult.Failure("Incorrect password for Client or account not found.");
        }

        _currentUser.User = user;
        return new LoginResult.Success();
    }

    public CreateUserResult Register(UserRole role, string username, string? password)
    {
        if (_userRepository.FindUserByName(username) != null)
            return new CreateUserResult.Failure("Username is already taken.");

        if (role == UserRole.Admin && string.IsNullOrEmpty(password))
            return new CreateUserResult.Failure("Admin password cannot be empty.");

        var newUser = new User(0, username, password ?? string.Empty, role);
        _userRepository.AddUser(newUser);
        return new CreateUserResult.Success();
    }

    public BalanceResult GetBalance(int id)
    {
        User? user = _currentUser.User;
        if (user == null || user.Role != UserRole.Client)
            return new BalanceResult.Failure("User is not authenticated or not a Client.");

        Account? account = _accountRepository.FindAccount(user.Id);
        if (account == null)
            return new BalanceResult.Failure("Account not found.");

        return new BalanceResult.Success(account.Balance);
    }

    public OperationResult Withdraw(double amount)
    {
        if (amount <= 0)
            return new OperationResult.Failure("Amount must be greater than zero.");

        User? user = _currentUser.User;
        if (user == null || user.Role != UserRole.Client)
            return new OperationResult.Failure("User is not authenticated or not a Client.");

        Account? account = _accountRepository.FindAccount(user.Id);

        if (account != null && account.Balance < amount)
            return new OperationResult.Failure("Insufficient balance.");

        if (account == null)
        {
            return new OperationResult.Failure("Account not found.");
        }

        _accountRepository.Withdraw(account.Number, amount);
        _operationRepository.AddOperation(user, new Operation(amount, TypeOfOperation.Withdrawal));

        return new OperationResult.Success();
    }

    public OperationResult Replenish(double amount)
    {
        if (amount < 0)
        {
            return new OperationResult.Failure("Amount cannot be negative.");
        }

        User? user = _currentUser.User;
        if (user == null || user.Role != UserRole.Client)
        {
            return new OperationResult.Failure("User is not authenticated or not a Client.");
        }

        Account? account = _accountRepository.FindAccount(user.Id);
        if (account == null)
        {
            return new OperationResult.Failure("Account not found.");
        }

        _accountRepository.Replenish(account.Number, amount);
        _operationRepository.AddOperation(user, new Operation(amount, TypeOfOperation.Replenishment));
        return new OperationResult.Success();
    }

    public OperationResult GetHistory()
    {
        User? user = _currentUser.User;
        if (user is null)
        {
            return new OperationResult.Failure("User is not available.");
        }

        return new OperationResult.UserOperations(_operationRepository.GetOperations(user));
    }
}
