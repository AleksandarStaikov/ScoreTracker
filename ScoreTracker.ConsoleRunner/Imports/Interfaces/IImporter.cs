using ScoreTracker.ConsoleRunner.Common.Communication;

namespace ScoreTracker.ConsoleRunner.Imports.Interfaces;

public interface IImporter
{
    bool CanImport(CommandBody commandBody);

    void Import(CommandBody commandBody);
}