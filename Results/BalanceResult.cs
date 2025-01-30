namespace Itmo.ObjectOrientedProgramming.Lab5.Results;

public abstract record BalanceResult
{
    public sealed record Success(double Money) : BalanceResult;

    public sealed record Failure(string Message) : BalanceResult;
}