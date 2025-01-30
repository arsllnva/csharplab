using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.OperationRep;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.RepositoriesClasses;

public class OperationRepository : IOperationRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public OperationRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<Operation> GetOperations(User user)
    {
        const string sql =
            """
            select *
            from operations
            WHERE account_id = : user.Id;
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", user.Id);

        using NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read() is not false)
        {
            yield return new Operation(
                reader.GetDouble(2),
                reader.GetFieldValue<TypeOfOperation>(3));
        }
    }

    public void AddOperation(User? user, Operation operation)
    {
        const string sql =
            """
            insert into public.operations
            values (default, :id, :amount, CAST(:type as transaction_type))
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        if (user != null) command.AddParameter("id", user.Id);
        command.AddParameter("amount", operation.Money);
        command.AddParameter("type", operation.Op);

        command.ExecuteNonQuery();
    }
}