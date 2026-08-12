namespace BrickNova.Game;

public class BrickConfig
{
    public Point Position { get; init; }

    public Size Size { get; init; }

    public int Points { get; init; }

    public BrickConfig(
        Point position,
        Size size,
        int points)
    {
        Position = position;
        Size = size;
        Points = points;
    }
}
