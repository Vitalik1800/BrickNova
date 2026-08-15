using BrickNova.Models;

namespace BrickNova.Tests;

public class GameProgressTests
{
    [Fact]
    public void Constructor_ShouldInitializeProgress()
    {
        GameProgress progress = 
            new GameProgress();

        Assert.NotNull(progress);
    }

    [Fact]
    public void Progress_ShouldStoreCurrentLevel()
    {
        GameProgress progress = new GameProgress
        {
            CurrentLevel = 10
        };

        Assert.Equal(
            10,
            progress.CurrentLevel
        );
    }

    [Fact]
    public void Progress_ShouldStoreScore()
    {
        GameProgress progress = new GameProgress
        {
            Score = 1500
        };

        Assert.Equal(
            1500,
            progress.Score
        );
    }

    [Fact]
    public void Progress_ShouldStoreLives()
    {
        GameProgress progress = new GameProgress
        {
            Lives = 2
        };

        Assert.Equal(
            2,
            progress.Lives
        );
    }

    [Fact]
    public void Progress_ShouldStoreCompleteGameState()
    {
        DateTime updatedAt = DateTime.Now;

        GameProgress progress = new GameProgress
        {
            Id = 1,
            CurrentLevel = 15,
            Score = 2500,
            Lives = 2,
            UpdatedAt = updatedAt
        };

        Assert.Equal(
            1,
            progress.Id
        );

        Assert.Equal(
            15,
            progress.CurrentLevel
        );

        Assert.Equal(
            2500,
            progress.Score
        );

        Assert.Equal(
            2,
            progress.Lives
        );

        Assert.Equal(
            updatedAt,
            progress.UpdatedAt
        );
    }
}
