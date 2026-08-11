using BrickNova.Entities;

namespace BrickNova.Game;

public class CollisionManager
{
    private const int FieldWidth = 800;
    private const int FieldHeight = 600;

    public CollisionResult Update(Ball ball, Paddle paddle, IEnumerable<Brick> bricks)
    {
        CheckWallCollision(ball);
        CheckPaddleCollision(ball, paddle);

        Brick? destroyedBrick = CheckBrickCollision(
            ball,
            bricks
        );

        bool ballLost = IsBallLost(ball);

        return new CollisionResult
        {
            DestroyedBrick = destroyedBrick,
            BallLost = ballLost
        };
    }

    private void CheckWallCollision(Ball ball)
    {
        if (ball.Position.X <= 0)
        {
            ball.Position = new PointF(
                0,
                ball.Position.Y
            );

            ball.Velocity = new PointF(
                Math.Abs(ball.Velocity.X),
                ball.Velocity.Y
            );
        }

        if (ball.Position.X + ball.Size.Width >= FieldWidth)
        {
            ball.Position = new PointF(
                FieldWidth - ball.Size.Width,
                ball.Position.Y
            );

            ball.Velocity = new PointF(
                -Math.Abs(ball.Velocity.X),
                ball.Velocity.Y
            );
        }

        if (ball.Position.Y <= 0)
        {
            ball.Position = new PointF(
                ball.Position.X,
                0
            );

            ball.Velocity = new PointF(
                ball.Velocity.X,
                Math.Abs(ball.Velocity.Y)
            );
        }
    }

    private void CheckPaddleCollision(Ball ball, Paddle paddle)
    {
        RectangleF ballBounds = new RectangleF(
            ball.Position,
            ball.Size
        );

        Rectangle paddleBounds = new Rectangle(
            paddle.Position,
            paddle.Size
        );

        if (!ballBounds.IntersectsWith(paddleBounds))
        {
            return;
        }

        if (ball.Velocity.Y <= 0)
        {
            return;
        }

        ball.Position = new PointF(
            ball.Position.X,
            paddle.Position.Y - ball.Size.Height
        );

        float paddleCenterX =
            paddle.Position.X + 
            paddle.Size.Width / 2f;

        float ballCenterX =
            ball.Position.X +
            ball.Size.Width / 2f;

        float hitPosition =
            ballCenterX - paddleCenterX;

        float normalizedHit =
            hitPosition /
            (paddle.Size.Width / 2f);

        normalizedHit = Math.Clamp(
            normalizedHit,
            -1f,
            1f
        );

        const float maxHorizontalSpeed = 5f;

        float velocityX = 
            normalizedHit * maxHorizontalSpeed;

        float velocityY =
            -Math.Abs(ball.Velocity.Y);

        ball.Velocity = new PointF(
            velocityX,
            velocityY
        );
    }

    private Brick? CheckBrickCollision(Ball ball, IEnumerable<Brick> bricks)
    {
        RectangleF ballBounds = new RectangleF(
            ball.Position,
            ball.Size
        );

        foreach (Brick brick in bricks)
        {
            if (brick.IsDestroyed)
            {
                continue;
            }

            Rectangle brickBounds = new Rectangle(
                brick.Position,
                brick.Size
            );

            if (!ballBounds.IntersectsWith(brickBounds))
            {
                continue;
            }

            brick.Destroy();

            ball.Velocity = new PointF(
                ball.Velocity.X,
                -ball.Velocity.Y
            );

            return brick;
        }

        return null;
    }

    private bool IsBallLost(Ball ball)
    {
        return ball.Position.Y + ball.Size.Height > FieldHeight;
    }
}

