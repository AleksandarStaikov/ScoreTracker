namespace ScoreTracker.ConsoleRunner.Common;

public static class CommandBodyExtensions
{
    public static bool HasFlag(this CommandBody commandBody, string flagName)
    {
        return commandBody.Flags.Any(x => x == flagName);
    }

    public static bool HasNamedArgument(this CommandBody commandBody, string flagName)
    {
        return commandBody.NamedArguments.ContainsKey(flagName);
    }
    public static bool HasNamedArgument(this CommandBody commandBody, string flagName, out string? value)
    {
        return commandBody.NamedArguments.TryGetValue(flagName, out value);
    }
}