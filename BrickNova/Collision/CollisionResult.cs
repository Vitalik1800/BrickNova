using BrickNova.Entities;

namespace BrickNova.Collision;

public class CollisionResult
{
    public Brick? DestroyedBrick {  get; init; }

    public bool BallLost { get; init; }

    public bool PaddleHit { get; init; }
}
