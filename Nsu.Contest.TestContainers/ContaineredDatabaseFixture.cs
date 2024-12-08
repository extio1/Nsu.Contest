namespace Nsu.Contest.TestContainers;

using Testcontainers.PostgreSql;
using System.Threading.Tasks;
using Xunit;

public class PostgresContaineredDatabaseFixture : IAsyncLifetime
{
    public PostgreSqlContainer Container { get; private set; }

    public PostgresContaineredDatabaseFixture()
    {
        Container = new PostgreSqlBuilder()
            .WithDatabase("contest")
            .WithUsername("extio1")
            .WithCleanUp(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await Container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Container.DisposeAsync();
    }
}
