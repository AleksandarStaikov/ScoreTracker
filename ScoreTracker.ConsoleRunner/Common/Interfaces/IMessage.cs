namespace ScoreTracker.ConsoleRunner.Common.Interfaces;

public interface IMessage
{
    string Value { get; set; }
    MessageSeverity Severity { get; set; }
}