using System.Drawing;

namespace BrickNova.Entities;

public class Paddle
{
    public Point Position { get; private set; }

    public Size Size { get; }

    public int Speed { get; }

    public Paddle()
    {
        Position = new Point(350, 540);
        Size = new Size(100, 20);
        Speed = 5;
    }

    public void MoveLeft()
    {
        int newX = Position.X - Speed;

        if (newX < 0)
        {
            newX = 0;
        }

        Position = new Point(newX, Position.Y);
    }

    public void MoveRight()
    {
        int newX = Position.X + Speed;

        int maxX = 800 - Size.Width;

        if (newX > maxX)
        {
            newX = maxX;
        }

        Position = new Point(newX, Position.Y);
    }

    public void Reset()
    {
        Position = new Point(350, 540);
    }
}
