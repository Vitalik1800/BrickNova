namespace BrickNova.Models;

public class GameProgress
{
    public int Id { get; set; }

    public int CurrentLevel { get; set; }
    public int Score { get; set; }
    public int Lives { get; set; }
    public DateTime UpdatedAt { get; set; }
}
