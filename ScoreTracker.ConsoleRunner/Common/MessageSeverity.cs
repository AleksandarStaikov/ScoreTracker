using CarRentalSystem.Domain.Common;
using System.Globalization;

namespace ScoreTracker.ConsoleRunner.Common;

public class MessageSeverity : Enumeration
{
    public static MessageSeverity Info = new MessageSeverity(1, nameof(Info));
    public static MessageSeverity Error = new MessageSeverity(2, nameof(Error));
    public static MessageSeverity Help = new MessageSeverity(3, nameof(Help));

    public MessageSeverity(int value) : base(value)
    {
    }

    public MessageSeverity(int value, string name) : base(value, name)
    { }
}