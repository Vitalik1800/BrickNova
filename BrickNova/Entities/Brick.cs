namespace BrickNova.Entities;

public class Brick
{
    public Point Position { get; private set; }
    public Size Size { get; }
    public bool IsDestroyed { get; private set; }
    public int Points { get; }

    public Brick(Point position, Size size, int points)
    {
        Position = position;
        Size = size;
        Points = points;
        IsDestroyed = false;
    }

    public void Destroy()
    {
        IsDestroyed = true;
    }
}
