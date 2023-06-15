using CarRentalSystem.Domain.Common;

namespace ScoreTracker.ConsoleRunner.Common;

public class MessageSeverity : Enumeration
{
    public static readonly MessageSeverity Info = new MessageSeverity(1, nameof(Info));
    public static readonly MessageSeverity Error = new MessageSeverity(2, nameof(Error));
    public static readonly MessageSeverity Help = new MessageSeverity(3, nameof(Help));
    public static readonly MessageSeverity Warning = new MessageSeverity(4, nameof(Warning));
    public static readonly MessageSeverity Success = new MessageSeverity(5, nameof(Success));

    public MessageSeverity(int value) : base(value)
    {
    }

    public MessageSeverity(int value, string name) : base(value, name)
    { }
}