using BrickNova.Entities;

namespace BrickNova.Game;

public class LevelManager
{
    private readonly List<Brick> _bricks;

    public IReadOnlyList<Brick> Bricks => _bricks;

    public LevelManager()
    {
        _bricks = new List<Brick>();

        CreateLevel();
    }

    private void CreateLevel()
    {
        _bricks.Add(
            new Brick(
                new Point(100, 100),
                new Size(80, 20),
                100
            )
        );
    }

    public bool IsLevelCompleted()
    {
        return _bricks.All(brick => brick.IsDestroyed);
    }
}
