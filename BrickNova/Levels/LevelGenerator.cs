using BrickNova.Entities;
using BrickNova.Game;

namespace BrickNova.Levels;

public static class LevelGenerator
{
    private const int BrickWidth = 80;
    private const int BrickHeight = 20;

    private const int StartY = 100;

    private const int HorizontalSpacing = 10;
    private const int VerticalSpacing = 10;

    private const int BrickPoints = 100;

    public static LevelConfig Generate(int level)
    {
        if (level < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(level),
                level,
                "Level must be greater than zero."
            );
        }

        List<BrickConfig> bricks = GeneratePattern(level);

        return new LevelConfig(
            levelNumber: level,
            bricks: bricks
        );
    }

    private static List<BrickConfig> GeneratePattern(int level)
    {
        int pattern = (level - 1) % 5;

        return pattern switch
        {
            0 => GenerateRectangle(level),
            1 => GeneratePyramid(level),
            2 => GenerateInvertedPyramid(level),
            3 => GenerateDiamond(level),
            4 => GenerateCross(level),

            _ => throw new InvalidOperationException()
        };
    }

    private static List<BrickConfig> GenerateRectangle(int level)
    {
        int rows = 2 + level / 10;
        int columns = 3 + level / 5;

        return GenerateGrid(rows, columns);
    }

    private static List<BrickConfig> GeneratePyramid(int level)
    {
        int rows = Math.Min(3 + level / 5, 8);

        List<BrickConfig> bricks = new();

        for (int row = 0; row < rows; row++)
        {
            int columns = row + 1;

            int totalWidth =
                columns * BrickWidth +
                (columns - 1) * HorizontalSpacing;

            int startX = 400 - totalWidth / 2;

            for (int column = 0; column < columns; column++)
            {
                bricks.Add(
                    CreateBrick(
                        startX + 
                        column * (BrickWidth + HorizontalSpacing),

                        StartY + 
                        row * (BrickHeight + VerticalSpacing)
                    )
                );
            }
        }

        return bricks;
    }

    private static List<BrickConfig> GenerateInvertedPyramid(int level)
    {
        int rows = Math.Min(3 + level / 5, 8);

        List<BrickConfig> bricks = new();

        for (int row = 0; row < rows; row++)
        {
            int columns = rows - row;

            int totalWidth = 
                columns * BrickWidth +
                (columns - 1) * HorizontalSpacing;

            int startX = 400 - totalWidth / 2;

            for (int column = 0; column < columns; column++)
            {
                bricks.Add(
                    CreateBrick(
                        startX +
                        column * (BrickWidth + HorizontalSpacing),

                        StartY + 
                        row * (BrickHeight + VerticalSpacing)   
                    )
                );
            }
        }

        return bricks;
    }

    private static List<BrickConfig> GenerateDiamond(int level)
    {
        int rows = Math.Min(3 + level / 5, 5);

        List<BrickConfig> bricks = new();

        for (int row = 0; row < rows; row++)
        {
            AddCenteredRow(
                bricks,
                row + 1,
                row
            );
        }

        for (int row = rows - 2; row >= 0; row--)
        {
            AddCenteredRow(
                bricks,
                row + 1,
                rows + (rows - 2 - row)
            );
        }

        return bricks;
    }

    private static List<BrickConfig> GenerateCross(int level)
    {
        List<BrickConfig> bricks = new();

        int centerX = 400;

        for (int row = 0; row < 5; row++)
        {
            bricks.Add(
                CreateBrick(
                    centerX - BrickWidth / 2,
                    StartY + 
                    row * (BrickHeight + VerticalSpacing)
                )
            );
        }

        for (int column = -2; column <= 2; column++)
        {
            if (column == 0)
            {
                continue;
            }

            bricks.Add(
                CreateBrick(
                    centerX -
                    BrickWidth / 2 +
                    column * (BrickWidth + HorizontalSpacing),

                    StartY +
                    2 * (BrickHeight + VerticalSpacing)
                )
            );
        }

        return bricks;
    }

    private static List<BrickConfig> GenerateGrid(
        int rows,
        int columns)
    {
        List<BrickConfig> bricks = new();

        int totalWidth =
            columns * BrickWidth +
            (columns - 1) * HorizontalSpacing;

        int startX = 400 - totalWidth / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                bricks.Add(
                    CreateBrick(
                        startX + 
                        column * (BrickWidth + HorizontalSpacing),

                        StartY +
                        row * (BrickHeight + VerticalSpacing)
                    )
                );
            }
        }

        return bricks;
    }

    private static void AddCenteredRow(
        List<BrickConfig> bricks,
        int columns,
        int row)
    {
        int totalWidth = 
            columns * BrickWidth +
            (columns - 1) * HorizontalSpacing;

        int startX = 400 - totalWidth / 2;

        for (int column = 0; column < columns; column++)
        {
            bricks.Add(
                CreateBrick(
                    startX + 
                    column * (BrickWidth + HorizontalSpacing),

                    StartY +
                    row * (BrickHeight + VerticalSpacing)
                )
            );
        }
    }

    private static BrickConfig CreateBrick(
        int x,
        int y)
    {
        return new BrickConfig(
            new Point(x, y),
            new Size(BrickWidth, BrickHeight),
            BrickPoints
        );
    }
}