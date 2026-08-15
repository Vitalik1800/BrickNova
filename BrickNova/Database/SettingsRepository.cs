using BrickNova.Models;
using Microsoft.Data.Sqlite;

namespace BrickNova.Database;

public class SettingsRepository
{
    private readonly DatabaseManager _databaseManager;

    public SettingsRepository(
        DatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public void Initialize()
    {
        using SqliteConnection connection = 
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command = 
            connection.CreateCommand();

        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Settings
            (
                Id INTEGER PRIMARY KEY,
                MasterVolume REAL NOT NULL,
                SoundEnabled INTEGER NOT NULL,
                UpdatedAt TEXT NOT NULL
            )
            """;

        command.ExecuteNonQuery();

        CreateDefaultSettings(connection);
    }
 
    private void CreateDefaultSettings(
        SqliteConnection connection)
    {
        using SqliteCommand command =
            connection.CreateCommand();

        command.CommandText =
            """
            INSERT OR IGNORE INTO Settings
            (
                Id,
                MasterVolume,
                SoundEnabled,
                UpdatedAt
            )
            VALUES
            (
                1,
                1.0,
                1,
                @updatedAt
            );
            """;

        command.Parameters.AddWithValue(
            "@updatedAt",
            DateTime.Now.ToString("O")
        );

        command.ExecuteNonQuery();
    }

    public GameSettings LoadSettings()
    {
        using SqliteConnection connection = 
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command =
            connection.CreateCommand();

        command.CommandText =
            """
            SELECT
                Id,
                MasterVolume,
                SoundEnabled,
                UpdatedAt
            FROM Settings
            WHERE Id = 1;
            """;

        using SqliteDataReader reader = 
            command.ExecuteReader();

        if (!reader.Read())
        {
            return new GameSettings();
        }

        return new GameSettings
        {
            Id = reader.GetInt32(0),

            MasterVolume = 
                Convert.ToSingle(
                    reader.GetDouble(1)
                ),

            SoundEnabled = 
                reader.GetInt32(2) == 1,

            UpdatedAt = 
                DateTime.Parse(
                    reader.GetString(3)
                )
        };
    }

    public void SaveSettings(
        GameSettings settings)
    {
        using SqliteConnection connection =
            _databaseManager.CreateConnection();

        connection.Open();

        using SqliteCommand command = 
            connection.CreateCommand();

        command.CommandText =
            """
            INSERT OR REPLACE INTO Settings
            (
                Id,
                MasterVolume,
                SoundEnabled,
                UpdatedAt
            )
            VALUES
            (
                @id,
                @masterVolume,
                @soundEnabled,
                @updatedAt
            );
            """;

        command.Parameters.AddWithValue(
            "@id",
            settings.Id
        );

        command.Parameters.AddWithValue(
            "@masterVolume",
            settings.MasterVolume
        );

        command.Parameters.AddWithValue(
            "@soundEnabled",
            settings.SoundEnabled ? 1 : 0
        );

        command.Parameters.AddWithValue(
            "@updatedAt",
            settings.UpdatedAt.ToString("O")
        );

        command.ExecuteNonQuery();
    }

}
