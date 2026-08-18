using BrickNova.Models;
using Microsoft.Data.Sqlite;

namespace BrickNova.Database;

public class GameProgressRepository
{
    private readonly DatabaseManager _databaseManager;

    public GameProgressRepository(
        DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public void SaveProgress(GameProgress progress)
    {
        const string sql = """
            INSERT INTO GameProgress
            (
                Id,
                CurrentLevel,
                Score,
                Lives,
                UpdatedAt
            )
            VALUES
            (
                1,
                $currentLevel,
                $score,
                $lives,
                $updatedAt
            )
            ON CONFLICT(Id)
            DO UPDATE SET
                CurrentLevel = excluded.CurrentLevel,
                Score = excluded.Score,
                Lives = excluded.Lives,
                UpdatedAt = excluded.UpdatedAt;
            """;

        using SqliteConnection connection = 
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command = 
            connection.CreateCommand();

        command.CommandText = sql;

        command.Parameters.AddWithValue(
            "$currentLevel",
            progress.CurrentLevel
        );

        command.Parameters.AddWithValue(
            "$score",
            progress.Score
        );

        command.Parameters.AddWithValue(
            "$lives",
            progress.Lives
        );

        command.Parameters.AddWithValue(
            "$updatedAt",
            progress.UpdatedAt.ToString("O")
        );

        command.ExecuteNonQuery();
    }

    public GameProgress? LoadProgress()
    {
        const string sql = """
            SELECT
                Id,
                CurrentLevel,
                Score,
                Lives,
                UpdatedAt
            FROM GameProgress
            WHERE Id = 1;
            """;

        using SqliteConnection connection =
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command =
            connection.CreateCommand();

        command.CommandText = sql;

        using SqliteDataReader reader =
            command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new GameProgress
        {
            Id = reader.GetInt32(0),
            CurrentLevel = reader.GetInt32(1),
            Score = reader.GetInt32(2),
            Lives = reader.GetInt32(3),
            UpdatedAt = DateTime.Parse(
                reader.GetString(4)
            )
        };
    }

    public void DeleteProgress()
    {
        const string sql = """
            DELETE FROM GameProgress
            WHERE Id = 1;
            """;

        using SqliteConnection connection =
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command = 
            connection.CreateCommand();

        command.CommandText = sql;

        command.ExecuteNonQuery();
    }
}
