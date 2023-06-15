using ScoreTracker.ConsoleRunner.Common.Interfaces;

namespace ScoreTracker.ConsoleRunner.Common;

internal class CommunicationHub : ICommunicationHub
{
    private readonly IList<Action<Message>> _subscribers = new List<Action<Message>>();

    public void PublichMessage(Message message)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Invoke(message);    
        }
    }

    public void PublichMessage(string value, MessageSeverity severity)
        => PublichMessage(new Message(value, severity));

    public void Subscribe(Action<Message> action)
    {
        if (!_subscribers.Contains(action))
        {
            _subscribers.Add(action);
        }
    }

    public void Unsubscribe(Action<Message> action)
    {
        if (_subscribers.Contains(action))
        {
            _subscribers.Remove(action);
        }
    }
}