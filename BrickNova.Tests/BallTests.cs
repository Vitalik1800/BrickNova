using BrickNova.Entities;
using System.Drawing;

namespace BrickNova.Tests;

public class BallTests
{
    [Fact]
    public void Constructor_ShouldInitializeBall()
    {
        Ball ball = new Ball();
        
        Assert.NotNull(ball);
    }

    [Fact]
    public void Reset_ShouldSetInitialPosition()
    {
        Ball ball = new Ball();

        ball.Reset();

        Assert.Equal(
            new PointF(400, 300),
            ball.Position
        );
    }

    [Fact]
    public void Reset_ShouldSetInitialVelocity()
    {
        Ball ball = new Ball();

        ball.Reset();

        Assert.Equal(
            new PointF(3, -3),
            ball.Velocity
        );
    }

    [Fact]
    public void Reset_ShouldSetInitialSize()
    {
        Ball ball = new Ball();

        ball.Reset();

        Assert.Equal(
            new SizeF(20, 20),
            ball.Size
        );
    }

    [Fact]
    public void Move_ShouldChangePosition()
    {
        Ball ball = new Ball();

        PointF initialPosition = ball.Position;

        ball.Move();

        Assert.NotEqual(
            initialPosition,
            ball.Position
        );
    }

    [Fact]
    public void Move_ShouldUpdatePositionByVelocity()
    {
        Ball ball = new Ball();

        ball.Move();

        Assert.Equal(
            new PointF(403, 297),
            ball.Position
        );
    }

    [Fact]
    public void Reset_ShouldRestoreInitialState()
    {
        Ball ball = new Ball();

        ball.Position = new PointF(100, 200);
        ball.Velocity = new PointF(-5, 7);
        ball.Size = new SizeF(50, 50);

        ball.Reset();

        Assert.Equal(
            new PointF(400, 300),
            ball.Position
        );

        Assert.Equal(
            new PointF(3, -3),
            ball.Velocity
        );

        Assert.Equal(
            new SizeF(20, 20),
            ball.Size
        );
    }
}
