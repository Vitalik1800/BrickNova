using BrickNova.Game;
using BrickNova.Models;

namespace BrickNova.Tests;

public class GameStateTests
{
    [Fact]
    public void MenuState_ShouldExist()
    {
        GameState state = GameState.Menu;

        Assert.Equal(
            GameState.Menu,
            state
        );
    }

    [Fact]
    public void PlayingState_ShouldExist()
    {
        GameState state = GameState.Playing;

        Assert.Equal(
            GameState.Playing,
            state
        );
    }

    [Fact]
    public void PausedState_ShouldExist()
    {
        GameState state = GameState.Paused;

        Assert.Equal(
            GameState.Paused,
            state
        );
    }

    [Fact]
    public void GameOverState_ShouldExist()
    {
        GameState state = GameState.GameOver;

        Assert.Equal(
            GameState.GameOver,
            state
        );
    }

    [Fact]
    public void VictoryState_ShouldExist()
    {
        GameState state = GameState.Victory;

        Assert.Equal(
            GameState.Victory,
            state
        );
    }

    [Fact]
    public void GameState_ShouldContainAllRequiredStates()
    {
        GameState[] states =
            Enum.GetValues<GameState>();

        Assert.Contains(
            GameState.Menu,
            states
        );

        Assert.Contains(
            GameState.Playing,
            states
        );

        Assert.Contains(
            GameState.Paused,
            states
        );

        Assert.Contains(
            GameState.GameOver,
            states
        );

        Assert.Contains(
            GameState.Victory,
            states
        );
    }

    [Fact]
    public void PlayingState_ShouldNotEqualOtherStates()
    {
        GameState state = GameState.Playing;

        Assert.NotEqual(
            GameState.Menu,
            state
        );

        Assert.NotEqual(
            GameState.Paused,
            state
        );

        Assert.NotEqual(
            GameState.GameOver,
            state
        );

        Assert.NotEqual(
            GameState.Victory,
            state
        );
    }

    [Fact]
    public void PausedState_ShouldNotEqualOtherStates()
    {
        GameState state = GameState.Paused;

        Assert.NotEqual(
            GameState.Menu,
            state
        );

        Assert.NotEqual(
            GameState.Playing,
            state
        );

        Assert.NotEqual(
            GameState.GameOver,
            state
        );

        Assert.NotEqual(
            GameState.Victory,
            state
        );
    }

    [Fact]
    public void GameOver_ShouldOccur_WhenLivesReachZero()
    {
        bool result =
            GameOverCondition.IsGameOver(0);

        Assert.True(result);
    }

    [Fact]
    public void GameOver_ShouldOccur_WhenLivesAreNegative()
    {
        bool result =
            GameOverCondition.IsGameOver(-1);

        Assert.True(result);
    }

    [Fact]
    public void GameOver_ShouldOccur_WhenLivesRemain()
    {
        bool result =
            GameOverCondition.IsGameOver(1);

        Assert.False(result);
    }

    [Fact]
    public void Victory_ShouldOccur_OnFinalLevel()
    {
        bool result =
            VictoryCondition.IsVictory(50, 50);

        Assert.True(result);
    }

    [Fact]
    public void Victory_ShouldNotOccur_BeforeFinalLevel()
    {
        bool result =
            VictoryCondition.IsVictory(49, 50);

        Assert.False(result);
    }

    [Fact]
    public void Victory_ShouldNotOccur_WhenLevelExceedsFinalLevel()
    {
        bool result =
            VictoryCondition.IsVictory(51, 50);

        Assert.False(result);
    }
}
