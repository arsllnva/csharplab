using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForAccounts;
using Itmo.ObjectOrientedProgramming.Lab5.Contracts.ForUsers;
using Itmo.ObjectOrientedProgramming.Lab5.Service.ForAccount;
using Itmo.ObjectOrientedProgramming.Lab5.Service.ForUser;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAccountService, AccountService>();

        collection.AddScoped<CurrentUser>();
        collection.AddScoped<ICurrentUserService>(
            p => p.GetRequiredService<CurrentUser>());

        return collection;
    }
}