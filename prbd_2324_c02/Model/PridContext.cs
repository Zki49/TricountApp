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

        modelBuilder.Entity<Template>()
             .HasMany(t => t.TemplateItems)
             .WithOne(ti => ti.template)
             .HasForeignKey(ti => ti.template);

        modelBuilder.Entity<Template>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Template>()
            .HasOne(t => t.Tricount)
            .WithMany(tc => tc.Templates)
            .HasForeignKey(t => t.TricountId);

        modelBuilder.Entity<Repartitions>()
            .HasKey(r => new { r.operations, r.user } );

        modelBuilder.Entity<Repartitions>()
            .HasOne(r => r.operations)
            .WithMany(o => o.repartitions)  
            .HasForeignKey(r => r.operations);

        modelBuilder.Entity<Repartitions>()
            .HasOne(r => r.user)
            .WithMany(u => u.repartitions)
            .HasForeignKey(r => r.user);
            
        modelBuilder.Entity<Operations>()
            .HasOne(o => o.user)
            .WithMany(u => u.operations)
            .HasForeignKey(o => o.user);

        modelBuilder.Entity<Operations>()
            .HasOne(o => o.Tricount)
            .WithMany(tr => tr.Operations);
            
            
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
    public DbSet<Operations> Operations => Set<Operations>();
    public DbSet<Repartitions> Repartitions => Set<Repartitions>();

    public object Templates { get; internal set; }
}