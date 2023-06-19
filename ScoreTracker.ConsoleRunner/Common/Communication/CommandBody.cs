namespace ScoreTracker.ConsoleRunner.Common.Communication;

public class CommandBody
{
    private readonly IList<string> _allArguments;
    private readonly IList<string> _positionalArguments = new List<string>();
    private readonly IList<string> _flags = new List<string>();
    private readonly IDictionary<string, string> _namedArguments = new Dictionary<string, string>();

    public CommandBody(string[] commandSegments)
    {
        CommandName = commandSegments[0].ToLower();
        _allArguments = commandSegments[1..];

        for (int i = 0; i < _allArguments.Count(); i++)
        {
            var segment = _allArguments[i];

            if (IsFlag(segment))
            {
                _flags.Add(segment.Substring(1));
                continue;
            }

            if (IsPositionalArgument(segment))
            {
                _positionalArguments.Add(segment);
                continue;
            }

            if (IsArgumentName(segment))
            {
                if (i + 1 == _allArguments.Count())
                {
                    throw new InvalidDataException($"Missing value for param '{segment}'"); ;
                }

                _namedArguments.Add(segment.Substring(2), _allArguments[i + 1]);
                i++;
                continue;
            }

            throw new InvalidDataException($"Command segment {segment} could not be interpreted");
        }
    }

    public string CommandName { get; private set; }

    public IReadOnlyCollection<string> Arguments => _allArguments.AsReadOnly();
    public IReadOnlyCollection<string> PositionalArguments => _positionalArguments.AsReadOnly();
    public IReadOnlyCollection<string> Flags => _flags.AsReadOnly();
    public IReadOnlyDictionary<string, string> NamedArguments => _namedArguments.AsReadOnly();


    private bool IsPositionalArgument(string segment) => !segment.StartsWith("-");

    private bool IsFlag(string segment) => segment.StartsWith("-") && !segment.StartsWith("--");

    private bool IsArgumentName(string segment) => segment.StartsWith("--") ;
}