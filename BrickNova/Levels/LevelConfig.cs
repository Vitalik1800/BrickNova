using BrickNova.Game;

namespace BrickNova.Levels;

public class LevelConfig
{
    public int LevelNumber { get; init; }

    public IReadOnlyList<BrickConfig> Bricks { get; init; } 

    public LevelConfig(
        int levelNumber,
        IReadOnlyList<BrickConfig> bricks )
    {
        LevelNumber = levelNumber;
        Bricks = bricks;
    }
}
