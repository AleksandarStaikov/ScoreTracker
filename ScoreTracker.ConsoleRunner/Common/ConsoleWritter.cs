using ScoreTracker.ConsoleRunner.Common.Interfaces;

namespace ScoreTracker.ConsoleRunner.Common;

public class ConsoleWritter : IConsoleWritter
{
    public void Write(Message message)
    {
        if (message.Severity == MessageSeverity.Info) { WriteInfoMessage(message); }
        if (message.Severity == MessageSeverity.Error) { WriteErrorMessage(message); }
        if (message.Severity == MessageSeverity.Help) { WriteHelpMessage(message); }
        if (message.Severity == MessageSeverity.Warning) { WriteWarningMessage(message); }
        if (message.Severity == MessageSeverity.Success) { WriteSucessMessage(message); }

        ResetConsole();
    }

    private void WriteInfoMessage(Message message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message.Value);
    }

    private void WriteErrorMessage(Message message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message.Value);
    }

    private void WriteHelpMessage(Message message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message.Value);
    }

    private void WriteWarningMessage(Message message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message.Value);
    }

    private void WriteSucessMessage(Message message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message.Value);
    }

    private void ResetConsole()
    {
        Console.ForegroundColor = ConsoleColor.White;
    }
}