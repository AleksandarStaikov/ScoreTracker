using ScoreTracker.Common.Models.Game;

namespace ScoreTracker.Common.Extensions
{
    public static class GameExtensions
    {

        public static bool HasWinner(this Game game)
        {
            return game.TotalBlackPoints >= GameConstants.GameWinningPoints || 
                   game.TotalRedPoints >= GameConstants.GameWinningPoints;
        }

        public static Game ComputeTotals(this Game game)
        {
            // TODO : This is a bit of a anti practice (modifying the input data in the method)
            // It would probably be a better idea to use two separate methods that returned the value.
            game.TotalRedPoints = game.RedPoints.Sum();
            game.TotalBlackPoints = game.BlackPoints.Sum();

            return game;
        }

        public static bool BlackTeamWins(this Game game)
        {
            return game.TotalBlackPoints > game.TotalRedPoints;
        }

        public static bool BlackTeamWins(this GameOverview game)
        {
            return game.TotalBlackPoints > game.TotalRedPoints;
        }
    }
}
