using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;

public interface IUserService
{
    LoginResult Login(string username, string password);

    CreateUserResult Register(UserRole role, string username, string? password);

    BalanceResult GetBalance(int id);

    OperationResult Withdraw(double amount);

    OperationResult Replenish(double amount);

    OperationResult GetHistory();
}