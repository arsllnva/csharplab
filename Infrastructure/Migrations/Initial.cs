using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
               create table oop(
                   user_id bigint primary key generated always as identity
               );

               create type user_role as enum
               (
                   'admin',
                   'client'
               );

               create type operation_type as enum
               (
                   'withdrawal',
                   'replenishment'
               );

               create table users
               (
                   user_id bigint primary key generated always as identity,
                   user_name text not null,
                   user_password text not null,
                   user_role user_role not null
               );

               create table accounts
               (
                   account_id bigint primary key generated always as identity,
                   account_number bigint not null,
                   account_balance numeric(100, 2) not null,
                   account_password text not null
               );

               create table operations
               (
                   operation_id bigint primary key generated always as identity,
                   account_id bigint not null references accounts(account_id),
                   operation_amount numeric(100, 2) not null,
                   operation_type operation_type not null
               );

               """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return """
                   drop table oop cascade;
                   drop table users cascade;
                   drop table accounts cascade;
                   drop table operations cascade;
                   drop type user_role;
                   drop type operation_type;
               """;
    }
}
