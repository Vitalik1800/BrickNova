using BrickNova.Database;
using BrickNova.Models;
using Microsoft.Data.Sqlite;

namespace BrickNova.IntegrationTests;

public class TestDataInitializationTests
{
    [Fact]
    public void Initialize_ShouldCreateTestDatabase()
    {
        string databasePath;

        DatabaseManager databaseManager =
            TestDataInitializer
                .CreateInitializedDatabase(
                    out databasePath
                );

        try
        {
            Assert.True(
                File.Exists(databasePath)
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void Initialize_ShouldCreatePersistenceTables()
    {
        string databasePath;

        DatabaseManager databaseManager =
            TestDataInitializer
                .CreateInitializedDatabase(
                    out databasePath
                );

        try
        {
            using SqliteConnection connection = 
                databaseManager.CreateConnection();

            connection.Open();

            using SqliteCommand command =
                connection.CreateCommand();

            command.CommandText =
                """
            SELECT name
            FROM sqlite_master
            WHERE type = 'table';
            """;

            using SqliteDataReader reader =
                command.ExecuteReader();

            List<string> tables = new();

            while (reader.Read())
            {
                tables.Add(
                    reader.GetString(0)
                );
            }

            Assert.Contains(
                "Scores",
                tables
            );

            Assert.Contains(
                "GameProgress",
                tables
            );

            Assert.Contains(
                "Settings",
                tables
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void CreateProgress_ShouldStoreTestProgress()
    {
        string databasePath;

        DatabaseManager databaseManager =
            TestDataInitializer
                .CreateInitializedDatabase(
                    out databasePath
                );

        TestDataInitializer.CreateProgress(
            databaseManager,
            level: 10,
            score: 1500,
            lives: 2
        );

        GameProgressRepository repository =
            new GameProgressRepository(
                databaseManager
            );

        GameProgress? progress =
            repository.LoadProgress();

        Assert.NotNull(progress);

        Assert.Equal(
            10,
            progress.CurrentLevel
        );

        Assert.Equal(
            1500,
            progress.Score
        );

        Assert.Equal(
            2,
            progress.Lives
        );
    }
}
