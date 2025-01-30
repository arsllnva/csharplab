using Itmo.ObjectOrientedProgramming.Lab5.Models.Operations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Results;

public abstract record OperationResult
{
    public sealed record Success : OperationResult;

    public sealed record Failure(string Message) : OperationResult;

    public record UserOperations(IEnumerable<Operation> Operations) : OperationResult;
}