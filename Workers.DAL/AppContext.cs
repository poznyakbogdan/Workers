using Microsoft.EntityFrameworkCore;
using Workers.DAL.Comparers;
using Workers.DAL.Converters;
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

        modelBuilder.Entity<Employee>(builder =>
        {
            builder.Property(x => x.DateOfBirth)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });
        
        modelBuilder.Entity<Employee>()
            .HasMany(t => t.Positions)
            .WithMany(s => s.Employees)
            .UsingEntity<Dictionary<string, object>>(
                "EmployeePosition",
                x => x.HasOne<Position>().WithMany().OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Employee>().WithMany().OnDelete(DeleteBehavior.Cascade)
            );
    }
}