using Microsoft.EntityFrameworkCore;
using Workers.DAL.Models;

namespace Workers.DAL;

public class AppContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }

    public AppContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var checkConstraintSql = $"{nameof(Position.Grade)} > 0 AND {nameof(Position.Grade)} < 16";
        modelBuilder.Entity<Position>()
            .HasCheckConstraint(nameof(Position.Grade), checkConstraintSql);
    }
}