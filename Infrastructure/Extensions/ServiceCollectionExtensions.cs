using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.RepositoriesClasses;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.AccountRep;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.OperationRep;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.UserRep;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatform();
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IAccountRepository, AccountRepository>();
        collection.AddScoped<IOperationRepository, OperationRepository>();

        return collection;
    }
}