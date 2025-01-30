namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;

public interface IScenario
{
    string Name { get; }

    void Run();
}