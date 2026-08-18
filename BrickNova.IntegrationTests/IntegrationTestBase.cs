using BrickNova.Database;

namespace BrickNova.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected DatabaseManager DatabaseManager { get; }

    protected string DatabasePath { get; }

    protected IntegrationTestBase()
    {
        DatabaseManager =
            TestDataInitializer
                .CreateInitializedDatabase(
                    out string databasePath   
                );

        DatabasePath = databasePath;
    }
}
