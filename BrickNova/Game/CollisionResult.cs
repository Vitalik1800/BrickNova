using BrickNova.Entities;

namespace BrickNova.Game;

public class CollisionResult
{
    public Brick? DestroyedBrick {  get; init; }

    public bool BallLost { get; init; }
}
