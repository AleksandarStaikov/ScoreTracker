namespace ScoreTracker.ConsoleRunner.Common.Communication;

public static class CommandBodyExtensions
{
    public static bool HasFlag(this CommandBody commandBody, string flagName) 
        => commandBody.Flags.Any(x => x == flagName);

    public static bool HasPositionalArgument(this CommandBody commandBody, string positionalArgumentValue)
        => commandBody.PositionalArguments.Contains(positionalArgumentValue);

    public static bool HasPositionalArgument(this CommandBody commandBody, string positionalArgumentValue, int index)
        => commandBody.PositionalArguments.Count() - 1 >= index &&
           commandBody.PositionalArguments.ElementAt(index) == positionalArgumentValue;

    public static bool HasNamedArgument(this CommandBody commandBody, string flagName) 
        => commandBody.NamedArguments.ContainsKey(flagName);

    public static bool HasNamedArgument(this CommandBody commandBody, string flagName, out string? value)
        => commandBody.NamedArguments.TryGetValue(flagName, out value);
}