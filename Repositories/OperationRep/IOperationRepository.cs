using Itmo.ObjectOrientedProgramming.Lab5.Models.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Repositories.OperationRep;

public interface IOperationRepository
{
    IEnumerable<Operation> GetOperations(User user);

    void AddOperation(User? user, Operation operation);
}