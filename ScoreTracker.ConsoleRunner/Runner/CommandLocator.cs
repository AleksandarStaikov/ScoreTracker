using ScoreTracker.ConsoleRunner.Runner.Interfaces;
using System.Reflection;

namespace ScoreTracker.ConsoleRunner.Runner;

public static class CommandLocator
{
    private static IList<Type> _commandTypes = null!;

    public static IEnumerable<Type> GetAllCommandsInAssembly()
    {
        if (_commandTypes == null)
        {
            _commandTypes = typeof(CommandLocator)
                .Assembly
                .GetTypes()
                .Where(TypeIsCommand)
                .ToList();
        }

        return _commandTypes.AsReadOnly();
    }

    private static bool TypeIsCommand(Type type)
        => type.IsAssignableTo(typeof(ICommand)) &&
        !type.IsAbstract &&
        !type.IsInterface;

}