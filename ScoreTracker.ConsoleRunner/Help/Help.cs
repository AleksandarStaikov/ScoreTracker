using ScoreTracker.ConsoleRunner.Common.Communication;
using ScoreTracker.ConsoleRunner.Common.Interfaces;
using ScoreTracker.ConsoleRunner.Runner;
using ScoreTracker.ConsoleRunner.Runner.Interfaces;
using System.Text;

namespace ScoreTracker.ConsoleRunner.Help;

public class Help : ICommand
{
    private readonly ICommunicationHub _communication;

    public Help(ICommunicationHub communication)
    {
        _communication = communication;
    }

    public void Execute(CommandBody commandSegments)
    {
        _communication.PublichMessage(GetMessage());
    }

    private string GetMessage()
    {
        var sb = new StringBuilder();

        sb.AppendLine("This is the help message :P, go read the code and figure it out on yourself, I ain`t helping you :)");
        sb.AppendLine("Jk :p, here you go with the list of commands:");
        sb.AppendLine("-----");

        foreach (var commandType in CommandLocator.GetAllCommandsInAssembly())
        {
            sb.AppendLine(commandType.Name);
        }

        sb.AppendLine("-----");
        sb.AppendLine("Add --help at the end of each command to get more information");

        return sb.ToString();
    }
}
