namespace BrickNova.Models;

public static class LifeManager
{
    public const int InitialLives = 3;

    public static int Decrement(int lives)
    {
        return lives - 1;
    }

    public static int Reset()
    {
        return InitialLives;
    }
}
