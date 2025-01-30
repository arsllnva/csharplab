using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Service.ForUser;

public class CurrentUser : ICurrentUserService
{
    public User? User { get; set; }
}