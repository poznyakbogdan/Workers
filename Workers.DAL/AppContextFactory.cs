using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace Workers.DAL;

public class AppContextFactory : IDesignTimeDbContextFactory<AppContext>
{
    private readonly string _connectionString;
    private readonly ILoggerFactory _loggerFactory;
    public AppContextFactory(string connectionString, ILoggerFactory loggerFactory)
    {
        _connectionString = connectionString;
        _loggerFactory = loggerFactory;
    }
    
    public AppContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder();
        builder
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(_loggerFactory)
            .UseMySQL(_connectionString);
        return new AppContext(builder.Options);
    }
}
