using BrickNova.Database;
using BrickNova.Models;

namespace BrickNova.IntegrationTests;

public static class TestDataInitializer
{
    public static DatabaseManager CreateInitializedDatabase(
        out string databasePath)
    {
        databasePath =
            TestEnvironment.CreateDatabasePath();

        DatabaseManager databaseManager = 
            new DatabaseManager(databasePath);

        databaseManager.Initialize();

        return databaseManager;
    }

    public static void CreateProgress(
        DatabaseManager databaseManager,
        int level, 
        int score,
        int lives)
    {
        GameProgressRepository repository =
            new GameProgressRepository(
                databaseManager
            );

        GameProgress progress = new GameProgress
        {
            Id = 1,
            CurrentLevel = level,
            Score = score,
            Lives = lives,
            UpdatedAt = DateTime.Now
        };

        repository.SaveProgress(progress);
    }

    public static void CreateScore(
        DatabaseManager databaseManager,
        string playerName,
        int score,
        int level)
    {
        ScoreRepository repository =
            new ScoreRepository(
                databaseManager
            );

        ScoreRecord record = new ScoreRecord
        {
            PlayerName = playerName,
            Score = score,
            Level = level,
            CreatedAt = DateTime.Now
        };

        repository.SaveScore(record);
    }
}
