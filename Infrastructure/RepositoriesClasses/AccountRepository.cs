using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Repositories.AccountRep;
using Itmo.ObjectOrientedProgramming.Lab5.Results;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.RepositoriesClasses;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Account? FindAccount(int number)
    {
        const string sql =
            """
            select account_id, account_number, account_type, account_name, balance
            from public.accounts
            WHERE account_number = number
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("number", number);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Account(
            Username: reader.GetString(0),
            Number: number,
            Pin: reader.GetString(1),
            Balance: reader.GetDouble(1));
    }

    public void AddAccount(string username, int number, string password, double balance)
    {
        const string sql =
            """
            insert into public.accounts
            values (:usernames, :numbers, :passwords, :balances)
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("usernames", username);
        command.AddParameter("numbers", number);
        command.AddParameter("passwords", password);
        command.AddParameter("balances", balance);

        command.ExecuteNonQuery();
    }

    public BalanceResult GetBalance(int id)
    {
        const string sql =
            """
            select balance from public.accounts
            WHERE account_id = :id
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", id);
        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return new BalanceResult.Failure("Mistake");
        double balance = reader.GetDouble(2);
        return new BalanceResult.Success(balance);
    }

    public void Withdraw(int id, double amount)
    {
        const string sql =
            """
            update public.accounts
            set account_balance = account_balance - :amount
            WHERE account_id = :id
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.Parameters.AddWithValue("amount", amount);

        command.ExecuteNonQuery();
    }

    public void Replenish(int id, double amount)
    {
        const string sql =
            """
            update public.accounts
            set account_balance = account_balance + :amount
            WHERE account_id = :id
            """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.Parameters.AddWithValue("amount", amount);

        command.ExecuteNonQuery();
    }
}