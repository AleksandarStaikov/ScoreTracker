namespace ScoreTracker.ConsoleRunner.Common;

public record Message
{
    public Message(string value) : 
        this(value, MessageSeverity.Info) { }

    public Message(string value, MessageSeverity severity)
    {
        Severity = severity;
        Value = value;
    }

    public string Value { get; set; }
    public MessageSeverity Severity { get; set; }

    public static implicit operator Message(string value) => new Message(value);
}