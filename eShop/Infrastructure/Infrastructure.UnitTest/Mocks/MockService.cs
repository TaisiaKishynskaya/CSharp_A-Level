using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.UnitTest.Mocks;

public class MockService : BaseDataService<MockDbContext>
{
    public MockService(
        IDbContextWrapper<MockDbContext> dbContextWrapper,
        ILogger<MockService> logger)
        : base(dbContextWrapper, logger)
    {
    }

    public async Task RunWithException()
    {
        await ExecuteSafeAsync<bool>(() => throw new Exception());
    }

    public async Task RunWithoutException()
    {
        await ExecuteSafeAsync<bool>(() => Task.FromResult(true));
    }
}