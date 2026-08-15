using BrickNova.Levels;

namespace BrickNova.Tests;

public class LevelManagerTests
{
    [Fact]
    public void Constructor_ShouldInitializeLevelManager()
    {
        LevelManager levelManager = 
            new LevelManager();

        Assert.NotNull(levelManager);
    }

    [Fact]
    public void Constructor_ShouldStartAtLevelOne()
    {
        LevelManager levelManager = 
            new LevelManager();

        Assert.Equal(
            1,
            levelManager.CurrentLevel
        );
    }

    [Fact]
    public void Constructor_ShouldCreateBricks()
    {
        LevelManager levelManager =
            new LevelManager();

        Assert.NotEmpty(
            levelManager.Bricks
        );
    }

    [Fact]
    public void LoadLevel_ShouldChangeCurrentLevel()
    {
        LevelManager levelManager = 
            new LevelManager();

        levelManager.LoadLevel(5);

        Assert.Equal(
            5,
            levelManager.CurrentLevel
        );
    }

    [Fact]
    public void LoadLevel_ShouldCreateBricks()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(5);

        Assert.NotEmpty(
            levelManager.Bricks
        );
    }

    [Fact]
    public void LoadLevel_ShouldLoadRequestedLevel()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(10);

        Assert.Equal(
            10,
            levelManager.CurrentLevel
        );
    }

    [Fact]
    public void LoadLevel_ShouldReplaceCurrentLevel()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(5);

        Assert.Equal(
            5,
            levelManager.CurrentLevel
        );

        levelManager.LoadLevel(10);

        Assert.Equal(
            10,
            levelManager.CurrentLevel
        );
    }

    [Fact]
    public void LoadLevel_ShouldGenerateBricks()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(10);

        Assert.NotEmpty(
            levelManager.Bricks
        );
    }

    [Fact]
    public void LoadLevel_ShouldGenerateNewBrickCollection()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(1);

        int firstLevelBrickCount = 
            levelManager.Bricks.Count;

        levelManager.LoadLevel(2);

        int secondLevelBrickCount =
            levelManager.Bricks.Count;

        Assert.True(
            firstLevelBrickCount > 0
        );

        Assert.True(
            secondLevelBrickCount > 0
        );
    }

    [Fact]
    public void LoadLevel_ShouldThrow_WhenLevelIsLessThanOne()
    {
        LevelManager levelManager =
            new LevelManager();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => levelManager.LoadLevel(0)
        );
    }

    [Fact]
    public void LoadLevel_ShouldThrow_WhenLevelExceedsFinalLevel()
    {
        LevelManager levelManager =
            new LevelManager();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => levelManager.LoadLevel(51)
        );
    }

    [Fact]
    public void LevelTransition_ShouldMoveToNextLevel()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(1);

        Assert.Equal(
            1,
            levelManager.CurrentLevel
        );

        levelManager.LoadLevel(
            levelManager.CurrentLevel + 1
        );

        Assert.Equal(
            2,
            levelManager.CurrentLevel
        );
    }

    [Fact]
    public void LevelTransition_ShouldSupportMultipleLevels()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(1);

        for (int level = 2; level <= 5; level++)
        {
            levelManager.LoadLevel(level);

            Assert.Equal(
                level,
                levelManager.CurrentLevel
            );
        }
    }

    [Fact]
    public void LevelTransition_ShouldAllowFinalLevel()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(49);
        levelManager.LoadLevel(50);

        Assert.Equal(
            50,
            levelManager.CurrentLevel
        );

        Assert.True(
            levelManager.IsFinalLevel
        );
    }

    [Fact]
    public void LevelTransition_ShouldNotAllowLevelAfterFinalLevel()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(50);

        Assert.Throws<ArgumentOutOfRangeException>(
            () => levelManager.LoadLevel(51)
        );

        Assert.Equal(
            50,
            levelManager.CurrentLevel
        );
    }

    [Fact]
    public void FinalLevel_ShouldBeRecognized()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(50);

        Assert.True(
            levelManager.IsFinalLevel
        );
    }

    [Fact]
    public void LevelBeforeFinal_ShouldNotBeFinalLevel()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(49);

        Assert.False(
            levelManager.IsFinalLevel
        );
    }

    [Fact]
    public void FinalLevel_ShouldHaveCorrectLevelNumber()
    {
        LevelManager levelManager =
            new LevelManager();

        levelManager.LoadLevel(50);

        Assert.Equal(
            50,
            levelManager.CurrentLevel
        );
    }

    [Fact]
    public void LoadLevel_ShouldAcceptValidBoundaryLevels()
    {
        LevelManager levelManager = 
            new LevelManager();

        levelManager.LoadLevel(1);

        Assert.Equal(
            1,
            levelManager.CurrentLevel
        );

        levelManager.LoadLevel(50);

        Assert.Equal(
            50,
            levelManager.CurrentLevel
        );
    }
}
