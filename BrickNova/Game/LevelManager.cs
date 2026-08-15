using BrickNova.Entities;

namespace BrickNova.Game;

public class LevelManager
{
    private readonly List<Brick> _bricks;

    public IReadOnlyList<Brick> Bricks => _bricks;

    private int _currentLevel = 1;
    public int CurrentLevel => _currentLevel;

    private const int FinalLevel = 3;

    public int TotalLevels => FinalLevel;

    public bool IsFinalLevel => 
        _currentLevel == FinalLevel;

    public LevelManager()
    {
        _bricks = new List<Brick>();

        CreateLevel();
    }

    private void CreateLevel()
    {
        LoadLevel(1);
    }

    public void LoadLevel(int level)
    {
        if (level < 1 || level > FinalLevel)
        {
            throw new ArgumentOutOfRangeException(
                nameof(level),
                level,
                $"Level must be between 1 and {FinalLevel}."
            );
        }

        LevelConfig config =
            LevelGenerator.Generate(level);

        ClearLevel();

        _currentLevel = level;

        GenerateBricks(config);
    }

    public void ResetLevel()
    {
        LoadLevel(_currentLevel);
    }

    private void GenerateBricks(LevelConfig config)
    { 

        foreach (BrickConfig brickConfig in config.Bricks)
        {
            Brick brick = new Brick(
                brickConfig.Position,
                brickConfig.Size,
                brickConfig.Points
            );

            _bricks.Add(brick);
        }
    }

    private void ClearLevel()
    {
        _bricks.Clear();
    }

    public bool IsLevelCompleted()
    {
        if (_bricks.Count == 0)
        {
            return false;
        }

        return _bricks.All(
            brick => brick.IsDestroyed
        );
    }

    public void Reset()
    {
        LoadLevel(_currentLevel);
    }
}
