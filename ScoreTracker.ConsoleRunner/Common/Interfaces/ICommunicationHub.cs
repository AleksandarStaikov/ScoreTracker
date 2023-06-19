using ScoreTracker.ConsoleRunner.Common.Communication;

namespace ScoreTracker.ConsoleRunner.Common.Interfaces;

public interface ICommunicationHub
{
    public void PublichMessage(Message message);
    public void PublichMessage(string value, MessageSeverity severity);
    public void Subscribe(Action<Message> action);
    public void Unsubscribe(Action<Message> action);
}