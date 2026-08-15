using BrickNova.Collision;
using BrickNova.Entities;
using System.Drawing;

namespace BrickNova.Tests;

public class CollisionTests
{
    [Fact]
    public void Constructor_ShouldInitializeCollisionManager()
    {
        CollisionManager collisionManager = 
            new CollisionManager();

        Assert.NotNull(collisionManager);
    }

    [Fact]
    public void Update_ShouldDetectPaddleCollision()
    {
        CollisionManager collisionManager = 
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(390, 530),
            Velocity = new PointF(0, 3)
        };

        Paddle paddle = new Paddle();

        CollisionResult result =
            collisionManager.Update(
                ball,
                paddle,
                Enumerable.Empty<Brick>()
            );

        Assert.True(result.PaddleHit);
    }

    [Fact]
    public void PaddleCollision_ShouldReverseVerticalVelocity()
    {
        CollisionManager collisionManager =
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(390, 530),
            Velocity = new PointF(0, 3)
        };

        Paddle paddle = new Paddle();

        collisionManager.Update(
            ball,
            paddle,
            Enumerable.Empty<Brick>()
        );

        Assert.Equal(
            -3,
            ball.Velocity.Y
        );
    }

    [Fact]
    public void PaddleCollision_ShouldPlaceBallAbovePaddle()
    {
        CollisionManager collisionManager = 
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(390, 530),
            Velocity = new PointF(0, 3)
        };

        Paddle paddle = new Paddle();

        collisionManager.Update(
            ball,
            paddle,
            Enumerable.Empty<Brick>()
        );

        Assert.Equal(
            paddle.Position.Y - ball.Size.Height,
            ball.Position.Y
        );
    }

    [Fact]
    public void Update_ShouldDetectBrickCollision()
    {
        CollisionManager collisionManager =
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(100, 100),
            Velocity = new PointF(0, 3),
            Size = new SizeF(20, 20)
        };

        Brick brick = new Brick(
            new Point(100, 110),
            new Size(100, 20),
            100
        );

        CollisionResult result =
            collisionManager.Update(
                ball,
                new Paddle(),
                [brick]
            );

        Assert.NotNull(result.DestroyedBrick);
        Assert.True(result.DestroyedBrick == brick);
    }

    [Fact]
    public void BrickCollision_ShouldDestroyBrick()
    {
        CollisionManager collisionManager = 
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(100, 100),
            Velocity = new PointF(0, 3),
            Size = new SizeF(20, 20)
        };

        Brick brick = new Brick(
            new Point(100, 110),
            new Size(100, 20),
            100
        );

        collisionManager.Update(
            ball,
            new Paddle(),
            [brick]
        );

        Assert.True(brick.IsDestroyed);
    }

    [Fact]
    public void BrickCollision_ShouldReverseVerticalVelocity()
    {
        CollisionManager collisionManager = 
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(100, 100),
            Velocity = new PointF(0, 3),
            Size = new SizeF(20, 20)
        };

        Brick brick = new Brick(
            new Point(100, 110),
            new Size(100, 20),
            100
        );

        collisionManager.Update(
            ball,
            new Paddle(),
            [brick]
        );

        Assert.Equal(
            -3,
            ball.Velocity.Y
        );
    }

    [Fact]
    public void Update_ShouldDetectBallLost()
    {
        CollisionManager collisionManager = 
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(100, 590),
            Size = new SizeF(20, 20),
            Velocity = new PointF(0, 3)
        };

        CollisionResult result =
            collisionManager.Update(
                ball,
                new Paddle(),
                Enumerable.Empty<Brick>()
            );

        Assert.True(result.BallLost);
    }

    [Fact]
    public void Update_ShouldNotDetectBallLost_AtBottomBoundary()
    {
        CollisionManager collisionManager = 
            new CollisionManager();

        Ball ball = new Ball
        {
            Position = new PointF(100, 580),
            Size = new SizeF(20, 20),
            Velocity = new PointF(0, 3)
        };

        CollisionResult result =
            collisionManager.Update(
                ball,
                new Paddle(),
                Enumerable.Empty<Brick>()
            );

        Assert.False(result.BallLost);
    }
}


