using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenario.LoginScenarios;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.ScenarioBase;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();

        return collection;
    }
}