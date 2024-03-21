using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRBD_Framework;
using System.Configuration;

namespace prbd_2324_c02.Model;

public class PridContext : DbContextBase
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);

        /*
         * SQLite
         */

        // var connectionString = ConfigurationManager.ConnectionStrings["SqliteConnectionString"].ConnectionString;
        // optionsBuilder.UseSqlite(connectionString);

        /*
         * SQL Server
         */

        var connectionString = ConfigurationManager.ConnectionStrings["MsSqlConnectionString"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);

        ConfigureOptions(optionsBuilder);
    }
    public static void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Subscription>()
            .HasKey(s => new { s.TricountId, s.UserId });

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Tricount)
            .WithMany(t => t.Subscriptions)
            .HasForeignKey(s => s.TricountId);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.User)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.UserId);
    }

    private static void ConfigureOptions(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseLazyLoadingProxies()
            .LogTo(Console.WriteLine, LogLevel.Information) // permet de visualiser les requêtes SQL générées par LINQ
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors() // attention : ralentit les requêtes
            ;
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Subscription > Subscriptions => Set<Subscription>();
    public DbSet<Template> Template => Set<Template>();
    public DbSet<Template_items> Template_items => Set<Template_items>();
    public DbSet<Tricount> Tricount => Set<Tricount>();

    public object Templates { get; internal set; }
}