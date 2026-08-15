namespace BrickNova.Models;

public static class VictoryCondition
{
    public static bool IsVictory(
        int currentLevel,
        int finalLevel)
    {
        return currentLevel == finalLevel;
    }
}
