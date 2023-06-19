using ScoreTracker.ConsoleRunner.Common.Communication;

namespace ScoreTracker.ConsoleRunner.Common.Interfaces
{
    public interface IConsoleWritter
    {
        void Write(Message message);
    }
}