namespace BrickNova.Models;

public static class GameOverCondition
{
    public static bool IsGameOver(int lives)
    {
        return lives <= 0;
    }
}
