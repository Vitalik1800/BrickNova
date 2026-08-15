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
        int level,
        GameState gameState)
    {
        DrawBackground(graphics);

        if (gameState == GameState.Menu)
        {
            DrawMainMenu(graphics);
            return;
        }

        DrawBricks(graphics, bricks);
        DrawPaddle(graphics, paddle);
        DrawBall(graphics, ball);
        DrawHUD(graphics, score, lives, level);

        if (gameState == GameState.Paused)
        {
            DrawPauseOverlay(graphics);
        }

        if (gameState == GameState.Victory)
        {
            DrawVictoryScreen(
                graphics,
                score,
                level
            );

            return;
        }

        if (gameState == GameState.GameOver)
        {
            DrawGameOverOverlay(graphics);
        }

        DrawGameState(graphics, gameState);

        if (gameState == GameState.GameOver ||
            gameState == GameState.Victory)
        {
            DrawFinalResult(
                graphics,
                score,
                level
            );
        }
    }

    private void DrawBackground(Graphics graphics)
    {
        graphics.Clear(Color.Black);
    }

    private void DrawMainMenu(Graphics graphics)
    {
        using Font titleFont = new Font(
            "Arial",
            32,
            FontStyle.Bold
        );

        using Font menuFont = new Font(
            "Arial",
            18,
            FontStyle.Bold
        );

        using Brush titleBrush =
            new SolidBrush(Color.White);

        using Brush menuBrush =
            new SolidBrush(Color.DeepSkyBlue);

        const string title = "BRICKNOVA";

        SizeF titleSize = graphics.MeasureString(
            title,
            titleFont
        );

        float titleX =
            (800 - titleSize.Width) / 2;

        graphics.DrawString(
            title,
            titleFont,
            titleBrush,
            new PointF(
                titleX,
                60
            )
        );

        string[] menuItems =
        {
            "CONTINUE GAME",
            "NEW GAME",
            "HIGH SCORES",
            "HELP",
            "ABOUT",
            "SETTINGS",
            "EXIT"
        };

        const float startY = 150;
        const float spacing = 45;

        for (int i = 0; i < menuItems.Length; i++)
        {
            SizeF textSize = graphics.MeasureString(
                menuItems[i],
                menuFont
            );

            float x =
                (800 - textSize.Width) / 2;

            float y = 
                startY + i * spacing;

            graphics.DrawString(
                menuItems[i],
                menuFont,
                menuBrush,
                new PointF(x, y)
            );
        }
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
        int lives,
        int level)
    {

        using Font font = new Font(
            "Arial",
            12,
            FontStyle.Regular
        );

        using Brush brush = new SolidBrush(Color.White);

        float width = graphics.VisibleClipBounds.Width;

        graphics.DrawString(
            $"Score: {score}",
            font,
            brush,
            new PointF(10, 10)
        );

        string levelText = $"Level: {level}";

        SizeF levelTextSize = graphics.MeasureString(
            levelText,
            font
        );

        float levelX =
            (width - levelTextSize.Width) / 2;

        graphics.DrawString(
            levelText,
            font,
            brush,
            new PointF(levelX, 10)
        );

        string livesText = $"Lives: {lives}";

        SizeF livesTextSize = graphics.MeasureString(
            livesText,
            font
        );

        float livesX =
            width - livesTextSize.Width - 10;

        graphics.DrawString(
            livesText,
            font,
            brush,
            new PointF(livesX, 10)
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
            GameState.Paused => Color.Gold,
            GameState.GameOver => Color.Red,
            GameState.Victory => Color.LimeGreen,
            _ => Color.White
        };

        using Brush brush = new SolidBrush(stateColor);

        string? message = gameState switch
        {
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

        float x =
            (graphics.VisibleClipBounds.Width -
             textSize.Width) / 2;

        float y =
            (graphics.VisibleClipBounds.Height -
             textSize.Height) / 2;

        graphics.DrawString(
            message,
            font,
            brush,
            new PointF(x, y)
        );

        if (gameState == GameState.Paused)
        {
            DrawPauseInstructions(
                graphics,
                smallFont,
                brush,
                y + textSize.Height + 20
            );

            return;
        }

        string? secondaryMessage = gameState switch
        {
            GameState.GameOver => "Press R to Restart",
            GameState.Victory => "Press R to Restart",
            _ => null
        };

        if (secondaryMessage == null)
        {
            return;
        }

        SizeF secondaryTextSize = graphics.MeasureString(
            secondaryMessage,
            smallFont
        );

        float secondaryX =
            (graphics.VisibleClipBounds.Width -
             secondaryTextSize.Width) / 2;

        float secondaryY =
            y + textSize.Height + 20;

        graphics.DrawString(
            secondaryMessage,
            smallFont,
            brush,
            new PointF(
                secondaryX,
                secondaryY
            )
        );
    }

    private void DrawPauseOverlay(Graphics graphics)
    {
        RectangleF area = graphics.VisibleClipBounds;

        using Brush overlayBrush = new SolidBrush(
            Color.FromArgb(160, Color.Black)
        );

        graphics.FillRectangle(
            overlayBrush,
            area
        );
    }

    private void DrawGameOverOverlay(Graphics graphics)
    {
        RectangleF area = graphics.VisibleClipBounds;

        using Brush overlayBrush = new SolidBrush(
            Color.FromArgb(180, Color.Black)
        );

        graphics.FillRectangle(
            overlayBrush,
            area
        );
    }

    private void DrawPauseInstructions(
        Graphics graphics,
        Font smallFont,
        Brush brush,
        float startY)
    {
        const string resumeMessage = "Space - Resume";
        const string restartMessage = "R - Restart";
        const string menuMessage = "M - Main Menu";

        SizeF resumeSize = graphics.MeasureString(
            resumeMessage,
            smallFont
        );

        SizeF restartSize = graphics.MeasureString(
            restartMessage,
            smallFont
        );

        SizeF menuSize = graphics.MeasureString(
            menuMessage,
            smallFont
        );

        float resumeX =
            (graphics.VisibleClipBounds.Width -
             resumeSize.Width) / 2;

        float restartX =
            (graphics.VisibleClipBounds.Width -
             restartSize.Width) / 2;

        float menuX =
            (graphics.VisibleClipBounds.Width -
             menuSize.Width) / 2;

        graphics.DrawString(
            resumeMessage,
            smallFont,
            brush,
            new PointF(
                resumeX,
                startY
            )
        );

        graphics.DrawString(
            restartMessage,
            smallFont,
            brush,
            new PointF(
                restartX,
                startY + 25
            )
        );

        graphics.DrawString(
            menuMessage,
            smallFont,
            brush,
            new PointF(
                menuX,
                startY + 50
            )
        );
    }

    private void DrawFinalResult(
        Graphics graphics,
        int score,
        int level)
    {
        using Font font = new Font(
            "Arial",
            18,
            FontStyle.Bold
        );

        using Brush brush = new SolidBrush(
            Color.White
        );

        string scoreText = $"Final Score: {score}";
        string levelText = $"Final Level: {level}";

        SizeF scoreSize = graphics.MeasureString(
            scoreText,
            font
        );

        SizeF levelSize = graphics.MeasureString(
            levelText,
            font
        );

        float scoreX =
            (graphics.VisibleClipBounds.Width -
             scoreSize.Width) / 2;

        float levelX =
            (graphics.VisibleClipBounds.Width -
             levelSize.Width) / 2;

        float centerY =
            graphics.VisibleClipBounds.Height / 2;

        float scoreY = centerY + 100;
        float levelY = scoreY + scoreSize.Height + 10;

        graphics.DrawString(
            scoreText,
            font,
            brush,
            new PointF(
                scoreX,
                scoreY
            )
        );

        graphics.DrawString(
            levelText,
            font,
            brush,
            new PointF(
                levelX,
                levelY
            )
        );
    }

    private void DrawVictoryScreen(
    Graphics graphics,
    int score,
    int level)
    {
        using Font titleFont = new Font(
            "Arial",
            30,
            FontStyle.Bold
        );

        using Font infoFont = new Font(
            "Arial",
            18,
            FontStyle.Bold
        );

        using Font instructionFont = new Font(
            "Arial",
            14,
            FontStyle.Bold
        );

        using Brush titleBrush = new SolidBrush(
            Color.LimeGreen
        );

        using Brush infoBrush = new SolidBrush(
            Color.White
        );

        string title = "VICTORY";

        SizeF titleSize = graphics.MeasureString(
            title,
            titleFont
        );

        float titleX =
            (graphics.VisibleClipBounds.Width -
             titleSize.Width) / 2;

        float titleY = 180;

        graphics.DrawString(
            title,
            titleFont,
            titleBrush,
            new PointF(
                titleX,
                titleY
            )
        );

        string scoreText =
            $"Final Score: {score}";

        SizeF scoreSize = graphics.MeasureString(
            scoreText,
            infoFont
        );

        float scoreX =
            (graphics.VisibleClipBounds.Width -
             scoreSize.Width) / 2;

        float scoreY =
            titleY + titleSize.Height + 25;

        graphics.DrawString(
            scoreText,
            infoFont,
            infoBrush,
            new PointF(
                scoreX,
                scoreY
            )
        );

        string levelText =
            $"Final Level: {level}";

        SizeF levelSize = graphics.MeasureString(
            levelText,
            infoFont
        );

        float levelX =
            (graphics.VisibleClipBounds.Width -
             levelSize.Width) / 2;

        float levelY =
            scoreY + scoreSize.Height + 10;

        graphics.DrawString(
            levelText,
            infoFont,
            infoBrush,
            new PointF(
                levelX,
                levelY
            )
        );

        string instruction =
            "R - Restart    M - Main Menu";

        SizeF instructionSize =
            graphics.MeasureString(
                instruction,
                instructionFont
            );

        float instructionX =
            (graphics.VisibleClipBounds.Width -
             instructionSize.Width) / 2;

        float instructionY =
            levelY + levelSize.Height + 30;

        graphics.DrawString(
            instruction,
            instructionFont,
            infoBrush,
            new PointF(
                instructionX,
                instructionY
            )
        );
    }

}
