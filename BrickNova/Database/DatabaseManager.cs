using Microsoft.Data.Sqlite;

namespace BrickNova.Database;

public class DatabaseManager
{
    private readonly string _connectionString;

    public DatabaseManager()
    {
        string databasePath = Path.Combine(
            AppContext.BaseDirectory,
            "bricknova.db"
        );

        _connectionString =
            $"Data Source={databasePath}";
    }

    public void Initialize()
    {
        using SqliteConnection connection =
           CreateConnection();

        CreateScoresTable(connection);
        CreateGameProgressTable(connection);
        CreateSettingsTable(connection); 
    }

    public SqliteConnection CreateConnection()
    {
        SqliteConnection connection =
            new SqliteConnection(_connectionString);

        connection.Open();

        return connection;
    }

    private void CreateScoresTable(
        SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS Scores
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PlayerName TEXT NOT NULL,
                Score INTEGER NOT NULL,
                Level INTEGER NOT NULL,
                CreatedAt TEXT NOT NULL
            );
            """;

        using SqliteCommand command = 
            connection.CreateCommand();

        command.CommandText = sql;

        command.ExecuteNonQuery();
    }

    private void CreateGameProgressTable(
        SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS GameProgress
            (
                Id INTEGER PRIMARY KEY,
                CurrentLevel INTEGER NOT NULL,
                Score INTEGER NOT NULL,
                Lives INTEGER NOT NULL,
                UpdatedAt TEXT NOT NULL
            );
            """;

        using SqliteCommand command =
            connection.CreateCommand();

        command.CommandText = sql;

        command.ExecuteNonQuery();
    }

    private void CreateSettingsTable(
        SqliteConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS Settings
            (
                Id INTEGER PRIMARY KEY,
                MasterVolume INTEGER NOT NULL,
                SoundEnabled INTEGER NOT NULL,
                UpdatedAt TEXT NOT NULL
            );
            """;

        using SqliteCommand command = 
            connection.CreateCommand();

        command.CommandText = sql;

        command.ExecuteNonQuery();
    }
}

