using JobTracker.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace JobTracker.Api.Tests;

// Starts the API for tests, but replaces local SQLite with a temporary in-memory database.
public class JobTrackerApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the AppDbContext registration that Program.cs configured for local SQLite.
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<IDbContextOptionsConfiguration<AppDbContext>>();

            // Register AppDbContext again, now using a test-only in-memory database.
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("JobTrackerTests"));
        });
    }
}