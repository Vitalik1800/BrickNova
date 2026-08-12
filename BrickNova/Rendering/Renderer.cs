using BrickNova.Entities;
using BrickNova.Game;

namespace BrickNova.Rendering;

public class Renderer
{
    public void Render(
        Graphics graphics,
        IReadOnlyList<Brick> bricks,
        Paddle paddle,
        Ball ball,
        int score,
        int lives,
        GameState gameState)
    {
        DrawBackground(graphics);
        DrawBricks(graphics, bricks);
        DrawPaddle(graphics, paddle);
        DrawBall(graphics, ball);
        DrawHUD(graphics, score, lives);
        DrawGameState(graphics, gameState);

        
    }

    private void DrawBackground(Graphics graphics)
    {
        graphics.Clear(Color.Black);
    }

    private void DrawBricks(
        Graphics graphics,
        IReadOnlyList<Brick> bricks)
    {
        using Brush brush = new SolidBrush(Color.DodgerBlue);

        foreach (Brick brick in bricks)
        {
            if (brick.IsDestroyed)
            {
                continue;
            }

            graphics.FillRectangle(
                brush,
                new Rectangle(
                    brick.Position,
                    brick.Size
                )
            );
        }
    }

    private void DrawPaddle(
        Graphics graphics,
        Paddle paddle
        )
    {
        using Brush brush = new SolidBrush(Color.Gold);

        graphics.FillRectangle(
            brush,
            new Rectangle(
                paddle.Position,
                paddle.Size
            )
        );
    }

    private void DrawBall(
        Graphics graphics,
        Ball ball)
    {
        using Brush brush = new SolidBrush(Color.White);

        graphics.FillEllipse(
            brush,
            new RectangleF(
                ball.Position,
                ball.Size
            )
        );
    }

    private void DrawHUD(
        Graphics graphics,
        int score,
        int lives)
    {

        using Font font = new Font(
            "Arial",
            12,
            FontStyle.Regular
        );

        using Brush brush = new SolidBrush(Color.White);

        graphics.DrawString(
            $"Score: {score}",
            font,
            brush,
            new PointF(10, 10)
        );

        graphics.DrawString(
            $"Lives: {lives}",
            font,
            brush,
            new PointF(700, 10)
        );
    }

    private void DrawGameState(
        Graphics graphics,
        GameState gameState)
    {

        using Font font = new Font(
            "Arial",
            24,
            FontStyle.Bold
        );

        using Font smallFont = new Font(
            "Arial",
            14,
            FontStyle.Bold
        );

        Color stateColor = gameState switch
        {
            GameState.Menu => Color.DeepSkyBlue,
            GameState.Paused => Color.Gold,
            GameState.GameOver => Color.Red,
            GameState.Victory => Color.LimeGreen,
            _ => Color.White
        };

        using Brush brush = new SolidBrush(stateColor);

        string? message = gameState switch
        {
            GameState.Menu => "PRESS SPACE TO START",
            GameState.Paused => "PAUSED",
            GameState.GameOver => "GAME OVER",
            GameState.Victory => "VICTORY",
            _ => null
        };

        if (message == null)
        {
            return;
        }

        SizeF textSize = graphics.MeasureString(
            message,
            font
        );

        float x = (800 - textSize.Width) / 2;
        float y = (600 - textSize.Height) / 2;

        graphics.DrawString(
            message,
            font,
            brush,
            new PointF(x, y)
        );

        if (gameState == GameState.Victory ||
            gameState == GameState.GameOver)
        {
            const string restartMessage = "Press R to Restart";

            SizeF restartTextSize = graphics.MeasureString(
                restartMessage,
                smallFont
            );

            float restartX = 
                (800 - restartTextSize.Width) / 2;

            float restartY = y + textSize.Height + 20;

            graphics.DrawString(
                restartMessage,
                smallFont,
                brush,
                new PointF(
                    restartX,
                    restartY
                )
            );
        }

    }

    
}
