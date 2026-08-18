using BrickNova.Models;
using Microsoft.Data.Sqlite;

namespace BrickNova.Database;

public class ScoreRepository
{
    private readonly DatabaseManager _databaseManager;

    public ScoreRepository(
        DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public void SaveScore(ScoreRecord record)
    {
        const string sql = """
            INSERT INTO Scores
            (
                PlayerName,
                Score,
                Level,
                CreatedAt
            )
            VALUES
            (
                $playerName,
                $score,
                $level,
                $createdAt
            );
            """;

        using SqliteConnection connection = 
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command = 
            connection.CreateCommand();

        command.CommandText = sql;

        command.Parameters.AddWithValue(
            "$playerName",
            record.PlayerName
        );

        command.Parameters.AddWithValue(
            "$score",
            record.Score
        );

        command.Parameters.AddWithValue(
            "$level",
            record.Level
        );

        command.Parameters.AddWithValue(
            "$createdAt",
            record.CreatedAt.ToString("O")
        );

        command.ExecuteNonQuery();
    }

    public List<ScoreRecord> GetHighScores(
        int limit = 10)
    {
        limit = Math.Max(1, limit);

        const string sql = """
            SELECT
                Id,
                PlayerName,
                Score,
                Level,
                CreatedAt
            FROM Scores
            ORDER BY 
                Score DESC,
                CreatedAt DESC
            LIMIT $limit;
            """;

        List<ScoreRecord> scores = new();

        using SqliteConnection connection =
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command =
            connection.CreateCommand();

        command.CommandText = sql;

        command.Parameters.AddWithValue(
            "$limit",
            limit
        );

        using SqliteDataReader reader = 
            command.ExecuteReader();

        while (reader.Read())
        {
            ScoreRecord record = new ScoreRecord
            {
                Id = reader.GetInt32(0),
                PlayerName = reader.GetString(1),
                Score = reader.GetInt32(2),
                Level = reader.GetInt32(3),
                CreatedAt = DateTime.Parse(
                    reader.GetString(4)
                )
            };

            scores.Add(record);
        }

        return scores;
    }

}
