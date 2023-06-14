namespace ScoreTracker.ConsoleRunner.Common;

public record Message
{
    public Message(string value) => new Message(value, MessageSeverity.Info);
    public Message(string value, MessageSeverity severity)
    {
        Severity = severity;
        Value = value;
    }

    public string Value { get; set; } = string.Empty;
    public MessageSeverity Severity { get; set; } = MessageSeverity.Info;

    public static implicit operator Message(string value) => new Message(value);
}