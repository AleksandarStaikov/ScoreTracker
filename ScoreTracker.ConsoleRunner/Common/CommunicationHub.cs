using ScoreTracker.ConsoleRunner.Common.Interfaces;

namespace ScoreTracker.ConsoleRunner.Common;

internal class CommunicationHub : ICommunicationHub
{
    public void PublichMessage(Message message)
    {
        throw new NotImplementedException();
    }

    public void Subscribe(Action<Message> action)
    {
        throw new NotImplementedException();
    }

    public void Unsubscribe(Action<Message> action)
    {
        throw new NotImplementedException();
    }
}