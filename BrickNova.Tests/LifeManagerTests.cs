using BrickNova.Models;

namespace BrickNova.Tests;

public class LifeManagerTests
{
    [Fact]
    public void Decrement_ShouldReduceLivesByOne()
    {
        int lives = 3;

        lives = LifeManager.Decrement(lives);

        Assert.Equal(
            2, 
            lives
        );
    }

    [Fact]
    public void Decrement_ShouldReduceOneLife()
    {
        int lives = 2;

        lives = LifeManager.Decrement(lives);

        Assert.Equal(
            1,
            lives
        );
    }

    [Fact]
    public void Decrement_ShouldAllowLivesToReachZero()
    {
        int lives = 1;

        lives = LifeManager.Decrement(lives);

        Assert.Equal(
            0,
            lives
        );
    }

    [Fact]
    public void Reset_ShouldRestoreInitialLives()
    {
        int lives = 0;

        lives = LifeManager.Reset();

        Assert.Equal(
            3,
            lives
        );
    }

    [Fact]
    public void Reset_ShouldRestoreLivesAfterLosses()
    {
        int lives = 3;

        lives = LifeManager.Decrement(lives);
        lives = LifeManager.Decrement(lives);

        lives = LifeManager.Reset();

        Assert.Equal(
            3,
            lives
        );
    }

    [Fact]
    public void Reset_ShouldAlwaysReturnInitialLives()
    {
        Assert.Equal(
            3,
            LifeManager.Reset()
        );

        Assert.Equal(
            3,
            LifeManager.Reset()
        );
    }
}
