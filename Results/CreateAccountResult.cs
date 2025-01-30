namespace Itmo.ObjectOrientedProgramming.Lab5.Results;

public abstract record CreateAccountResult
{
    public sealed record Success(double Id) : CreateAccountResult;

    public sealed record Failure(string Message) : CreateAccountResult;
}