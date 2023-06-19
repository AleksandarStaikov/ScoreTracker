using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ScoreTracker.Common.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAsSingleton<TType>(
        this IServiceCollection services,
        Assembly assembly,
        bool registerAsBaseType = true) 
        => AddOfType<TType>(
            services,
            assembly,
            registerAsBaseType,
            (_, interfaceType, implementationType)
                => services.AddSingleton(interfaceType, implementationType));

    public static IServiceCollection AddOfType<TType>(this IServiceCollection services,
        Assembly assembly,
        bool registerBaseType,
        Action<IServiceCollection, Type, Type> registrationCallback)
    {
        var implementationTypes = GetImplementationClassesOfType<TType>(assembly);

        foreach ( var implementationType in implementationTypes )
        {
            var interfaceType = registerBaseType  ? typeof(TType) : implementationType;

            registrationCallback(services, interfaceType, implementationType);
        }

        return services;
    }

    public static IEnumerable<Type> GetImplementationClassesOfType<TType>(Assembly assembly) 
        => assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(TType)))
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToList();
}