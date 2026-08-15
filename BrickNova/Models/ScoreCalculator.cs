namespace BrickNova.Models;

public static class ScoreCalculator
{
    public static int AddPoints(
        int currentScore,
        int points)
    {
        return currentScore + points;
    }
}
