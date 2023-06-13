namespace ScoreTracker.ConsoleRunner.Imports
{
    public interface IDataSeeder
    {
        Task AddGames(int n, string teamId, string userAuthId);
    }
}