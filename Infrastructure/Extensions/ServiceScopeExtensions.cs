using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Extensions;

public static class ServiceScopeExtensions
{
    public static void UseInfrastructureDataAccess(this IServiceScope scope)
    {
        scope.UsePlatformMigrationsAsync(default).GetAwaiter().GetResult();
    }
}