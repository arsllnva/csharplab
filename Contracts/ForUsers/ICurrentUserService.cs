using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;

public interface ICurrentUserService
{
    User? User { get; set; }
}