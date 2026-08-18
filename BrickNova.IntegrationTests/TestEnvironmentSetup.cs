using BrickNova.Database;

namespace BrickNova.IntegrationTests;

public abstract class TestEnvironmentSetup
{
    protected DatabaseManager DatabaseManager { get; }

    protected string DatabasePath { get; }

    protected TestEnvironmentSetup()
    {
        DatabasePath =
            TestEnvironment.CreateDatabasePath();

        DatabaseManager = 
            new DatabaseManager(
                DatabasePath
            );

    }

    public void Dispose()
    {
        TestEnvironment.CleanupDatabase(
            DatabasePath
        );
    }
}
