namespace Itmo.ObjectOrientedProgramming.Lab5.Results;

public abstract record CreateUserResult
{
    public sealed record Success : CreateUserResult;

    public sealed record Failure(string Message) : CreateUserResult;
}