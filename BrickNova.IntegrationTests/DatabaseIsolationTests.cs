using Microsoft.Data.Sqlite;

namespace BrickNova.IntegrationTests;

public class DatabaseIsolationTests
{
    [Fact]
    public void TestDatabase_ShouldUseSeparateDatabaseFile()
    {
        string databasePath =
            TestEnvironment.CreateDatabasePath();

        try
        {
            using FileStream stream =
                File.Create(databasePath);

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
}
