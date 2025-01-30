using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Repositories.UserRep;

public interface IUserRepository
{
    User? FindUserByName(string name);

    void AddUser(User user);
}