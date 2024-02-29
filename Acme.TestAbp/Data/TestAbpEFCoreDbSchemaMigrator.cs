using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Acme.TestAbp.Data;

public class TestAbpEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public TestAbpEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the TestAbpDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TestAbpDbContext>()
            .Database
            .MigrateAsync();
    }
}
