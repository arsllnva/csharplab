using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<UserRole>();
    }
}