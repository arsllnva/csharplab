using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.UserRep;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.RepositoriesClasses;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? FindUserByName(string name)
    {
        const string sql =
            """
            select user_id, user_name, user_password, user_role 
            from users
            where user_name = :name;
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("name", name);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new User(
            Id: reader.GetInt32(0),
            Username: reader.GetString(1),
            SystemPassword: reader.GetString(2),
            Role: reader.GetFieldValue<UserRole>(3));
    }

    public void AddUser(User user)
    {
        const string sql =
            """
            insert into public.users
            values ( :id, :name, :password, :role);
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", user.Id);
        command.AddParameter("name", user.Username);
        command.AddParameter("password", user.SystemPassword);
        command.AddParameter("role", user.Role);

        command.ExecuteNonQuery();
    }
}