namespace BrickNova.Entities;

public class Ball
{
    public PointF Position { get; set; }
    public PointF Velocity { get; set; }
    public SizeF Size { get; set; }

    public void Move()
    {
        Position = new PointF(
            Position.X + Velocity.X,
            Position.Y + Velocity.Y
        );
    }

    public void Reset()
    {
        Position = new PointF(400, 300);
        Velocity = new PointF(3, -3);
    }
}
