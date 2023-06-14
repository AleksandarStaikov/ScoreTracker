namespace ScoreTracker.ConsoleRunner.Common.Interfaces;

public interface ICommunicationHub
{
    public void PublichMessage(Message message);
    public void Subscribe(Action<Message> action);
    public void Unsubscribe(Action<Message> action);
}