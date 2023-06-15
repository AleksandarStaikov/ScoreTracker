using ScoreTracker.ConsoleRunner.Runner.Interfaces;

namespace ScoreTracker.ConsoleRunner.Runner;

// TODO : Might need to convert it to normal class to use with DI for testing
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