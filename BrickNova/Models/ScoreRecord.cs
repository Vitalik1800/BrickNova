namespace BrickNova.Models;

public class ScoreRecord
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int Score { get; set; }
    public int Level { get; set; }
    public DateTime CreatedAt { get; set; }
}
