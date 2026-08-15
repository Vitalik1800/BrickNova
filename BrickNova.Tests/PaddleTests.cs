using BrickNova.Entities;
using System.Drawing;

namespace BrickNova.Tests;

public class PaddleTests
{
    [Fact]
    public void Constructor_ShouldInitializePaddle()
    {
        Paddle paddle = new Paddle();
        
        Assert.NotNull(paddle);
    }

    [Fact]
    public void Constructor_ShouldSetInitialPosition()
    {
        Paddle paddle = new Paddle();

        Assert.Equal(
            new Point(350, 540),
            paddle.Position
        );
    }

    [Fact]
    public void Constructor_ShouldSetSize()
    {
        Paddle paddle = new Paddle();

        Assert.Equal(
            new Size(100, 20),
            paddle.Size
        );
    }

    [Fact]
    public void Constructor_ShouldSetSpeed()
    {
        Paddle paddle = new Paddle();

        Assert.Equal(
            5,
            paddle.Speed
        );
    }

    [Fact]
    public void MoveLeft_ShouldMovePaddleLeft()
    {
        Paddle paddle = new Paddle();

        paddle.MoveLeft();

        Assert.Equal(
            new Point(345, 540),
            paddle.Position
        );
    }

    [Fact]
    public void MoveRight_ShouldMovePaddleRight()
    {
        Paddle paddle = new Paddle();

        paddle.MoveRight();

        Assert.Equal(
            new Point(355, 540),
            paddle.Position
        );
    }

    [Fact]
    public void Reset_ShouldRestoreInitialPosition()
    {
        Paddle paddle = new Paddle();

        paddle.MoveLeft();
        paddle.MoveRight();

        paddle.Reset();

        Assert.Equal(
            new Point(350, 540),
            paddle.Position
        );
    }
}
