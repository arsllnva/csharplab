namespace Itmo.ObjectOrientedProgramming.Lab5.Results;

public abstract record LoginResult
{
    public sealed record Success : LoginResult;

    public sealed record Failure(string Message) : LoginResult;
}