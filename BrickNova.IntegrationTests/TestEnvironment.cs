using BrickNova.Database;

namespace BrickNova.IntegrationTests;

public static class TestEnvironment
{
    public static string CreateDatabasePath()
    {
        string fileName =
            $"bricknova-test-{Guid.NewGuid():N}.db";

        return Path.Combine(
            AppContext.BaseDirectory,
            fileName
        );
    }

    public static void CleanupDatabase(
        string databasePath)
    {
        DeleteFileIfExists(databasePath);
        DeleteFileIfExists(databasePath + "-wal");
        DeleteFileIfExists(databasePath + "-shm");
    }

    private static void DeleteFileIfExists(
        string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        try
        {
            File.Delete(path);
        }
        catch (IOException)
        {

        }
        catch (UnauthorizedAccessException)
        {

        }
    }
}
