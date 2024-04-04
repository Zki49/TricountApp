using CalcBinding.Trace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRBD_Framework;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

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
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
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
             .HasForeignKey(ti => ti.templateId);

        modelBuilder.Entity<Template>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Template>()
            .HasOne(t => t.Tricount)
            .WithMany(tc => tc.Templates)
            .HasForeignKey(t => t.TricountId);

        modelBuilder.Entity<Repartitions>()
            .HasKey(r => new { r.operationsID, r.userId } );

        modelBuilder.Entity<Repartitions>()
            .HasOne(r => r.operations)
            .WithMany(o => o.repartitions)  
            .HasForeignKey(r => r.operationsID);

        modelBuilder.Entity<Repartitions>()
            .HasOne(r => r.user)
            .WithMany(u => u.repartitions)
            .HasForeignKey(r => r.userId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Operations>()
            .HasOne(o => o.user)
            .WithMany(u => u.operations)
            .HasForeignKey(o => o.userId);

        modelBuilder.Entity<Operations>()
            .HasOne(o => o.Tricount)
            .WithMany(tr => tr.Operations)
            .HasForeignKey(o => o.tricountId);
        

        modelBuilder.Entity<Template_items>()
            .HasKey(ti => new { ti.userId,ti.templateId});
        modelBuilder.Entity<Tricount>()
            .HasOne(t=>t.Creator)
            .WithMany(c=>c.tricounts)
            .HasForeignKey(t=>t.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);

  


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
    public DbSet<Tricount> Tricounts => Set<Tricount>();
    public DbSet<Operations> Operations => Set<Operations>();
    public DbSet<Repartitions> Repartitions => Set<Repartitions>();

    public object Templates { get; internal set; }

    public  void seedData() {
        Database.BeginTransaction();

        var user1 = new User { UserId = 1, FullName = "Boris", hashedPassword = "3D4AEC0A9B43782133B8120B2FDD8C6104ABB513FE0CDCD0D1D4D791AA42E338:C217604FDAEA7291C7BA5D1D525815E4:100000:SHA256", mail = "boverhaegen@epfc.eu", Role = false };
        var user2 = new User { UserId = 2, FullName = "Benoît", hashedPassword = "9E58D87797C6795D294E6762B6C05116D075BC18445AD4078C25674809DB57EF:C91E0B85B7264877C0424D52494D6296:100000:SHA256", mail = "bepenelle@epfc.eu", Role = false };
        var user3 = new User { UserId = 4, FullName = "Xavier", hashedPassword = "5B979AB86EC73B0996F439D0BC3947ECCFA0A41310C77533EA36CB409DBB1243:0CF43009110DE4B4AA6D4E749F622755:100000:SHA256", mail = "xapigeolet@epfc.eu", Role = false };
        var user4 = new User { UserId = 5, FullName = "Marc", hashedPassword = "955F147CE3473774E35EE58F4233AA84AE9118C6ECD4699DD788B8D588238034:5514D1DD0A97E9BA7FE4C0B5A4E89351:100000:SHA256", mail = "mamichel@epfc.eu", Role = false };
        var user5 = new User { UserId = 6, FullName = "Administrator", hashedPassword = "C9949A02A5DFBE50F1DA289DC162E3C97443AB09CE6F6EB1FD0C9D51B5241BBD:5533437973C5BC6459DB687CA5BDE76C:100000:SHA256", mail = "admin@epfc.eu", Role = true };

        Users.AddRange(user1, user2, user3, user4, user5);


        var tricount1 = new Tricount { Id = 1, Title = "Gers 2023" ,CreatorId = 1, CreatedAt = new DateTime(2023, 10, 10, 18, 42, 24) };
        var tricount2 = new Tricount { Id = 2, Title = "Resto badminton ", CreatorId = 1, CreatedAt = new DateTime(2023, 10, 10, 19, 25, 10) };
        var tricount3 = new Tricount { Id = 3, Title = "Vacances A la mer du nord", CreatorId = 1, CreatedAt = new DateTime(2023, 10, 10, 19, 31, 9) };
        var tricount4 = new Tricount { Id = 4 , Title = "Grosse virée    A Torremolinos  ", CreatorId = 2, CreatedAt = new DateTime(2023, 8, 15, 10, 0, 0) };
        var tricount5 = new Tricount { Id = 5, Title = "Torhout Werchter ", CreatorId = 3, CreatedAt = new DateTime(2023, 6, 2, 18, 30, 12) };

        Tricounts.AddRange(tricount1, tricount2, tricount3, tricount4, tricount5);

        var subscriptions = new List<Subscription>
 {
        new Subscription { TricountId = 1, UserId = 1 },
        new Subscription { TricountId = 1, UserId = 2 },
        new Subscription { TricountId = 1, UserId = 4 },
        new Subscription { TricountId = 1, UserId = 6 },
        new Subscription { TricountId = 2, UserId = 2 },
        new Subscription { TricountId = 2, UserId = 4 },
        new Subscription { TricountId = 2, UserId = 5 },
        new Subscription { TricountId = 2, UserId = 6 },
        new Subscription { TricountId = 3, UserId = 4 },
        new Subscription { TricountId = 3, UserId = 5 },
        new Subscription { TricountId = 3, UserId = 6 },
        new Subscription { TricountId = 4, UserId = 4 },
        new Subscription { TricountId = 4, UserId = 5 },
        new Subscription { TricountId = 4, UserId = 6 }
    };

        Subscriptions.AddRange(subscriptions);

        var operations = new List<Operations>
   {
        new Operations { OperationsId = 1 , title = "Colruyt", tricountId = 1, Amount = 100, DateTime = new DateTime(2023, 10, 13), userId = 2 },
        new Operations { OperationsId = 2 , title = "Plein essence", tricountId = 1, Amount = 75, DateTime = new DateTime(2023, 10, 13), userId = 1 },
        new Operations { OperationsId = 3 , title = "Grosses courses LIDL", tricountId = 1, Amount = 212.47, DateTime = new DateTime(2023, 10, 13), userId = 3 },
        new Operations { OperationsId = 4 , title = "Apéros", tricountId = 1, Amount = 31.89745622, DateTime = new DateTime(2023, 10, 13), userId = 1 },
        new Operations { OperationsId = 5 , title = "Boucherie", tricountId = 1, Amount = 25.5, DateTime = new DateTime(2023, 10, 26), userId = 2 },
        new Operations { OperationsId = 6 , title = "Loterie", tricountId = 1, Amount = 35, DateTime = new DateTime(2023, 10, 26), userId = 1 },
        new Operations { OperationsId = 7 , title = "Sangria", tricountId = 2, Amount = 42, DateTime = new DateTime(2023, 08, 16), userId = 2 },
        new Operations { OperationsId = 8 , title = "Jet Ski", tricountId = 2, Amount = 250, DateTime = new DateTime(2023, 08, 17), userId = 3 },
        new Operations { OperationsId = 9 , title = "PV parking", tricountId = 2, Amount = 15.5, DateTime = new DateTime(2023, 08, 16), userId = 3 },
        new Operations { OperationsId = 10 , title = "Tickets", tricountId = 3, Amount = 220, DateTime = new DateTime(2023, 06, 08), userId = 1 },
        new Operations { OperationsId = 11 , title = "Décathlon", tricountId = 3, Amount = 199.99, DateTime = new DateTime(2023, 07, 01), userId = 2 }
    };

        Operations.AddRange(operations);

        var repartitions = new List<Repartitions>
    {
        new Repartitions { weight = 1, userId = 1, operationsID = 1 },
        new Repartitions { weight = 1, userId = 2, operationsID = 1 },
        new Repartitions { weight = 1, userId = 1, operationsID = 2 },
        new Repartitions { weight = 1, userId = 2, operationsID = 2 },
        new Repartitions { weight = 2, userId = 1, operationsID = 3 },
        new Repartitions { weight = 1, userId = 2, operationsID = 3 },
        new Repartitions { weight = 1, userId = 3, operationsID = 3 },
        new Repartitions { weight = 1, userId = 1, operationsID = 4 },
        new Repartitions { weight = 2, userId = 2, operationsID = 4 },
        new Repartitions { weight = 3, userId = 3, operationsID = 4 },
        new Repartitions { weight = 2, userId = 1, operationsID = 5 },
        new Repartitions { weight = 1, userId = 2, operationsID = 5 },
        new Repartitions { weight = 1, userId = 3, operationsID = 5 },
        new Repartitions { weight = 1, userId = 1, operationsID = 6 },
        new Repartitions { weight = 1, userId = 3, operationsID = 6 },
        new Repartitions { weight = 1, userId = 2, operationsID = 7 },
        new Repartitions { weight = 2, userId = 3, operationsID = 7 },
        new Repartitions { weight = 3, userId = 4, operationsID = 7 },
        new Repartitions { weight = 2, userId = 3, operationsID = 8 },
        new Repartitions { weight = 1, userId = 4, operationsID = 8 },
        new Repartitions { weight = 1, userId = 2, operationsID = 9 },
        new Repartitions { weight = 5, userId = 4, operationsID = 9 },
        new Repartitions { weight = 1, userId = 1, operationsID = 10 },
        new Repartitions { weight = 1, userId = 3, operationsID = 10 },
        new Repartitions { weight = 2, userId = 2, operationsID = 11 },
        new Repartitions { weight = 2, userId = 4, operationsID = 11 }
    };

        Repartitions.AddRange(repartitions);

        var templates = new List<Template>
        {
        new Template { Id = 1 , Title = "Boris paye double", TricountId = 4 },
        new Template { Id = 2 , Title = "Benoît ne paye rien", TricountId = 4 }
        };

    

        Template.AddRange(templates);

        var templateItems = new List<Template_items>
    {
        new Template_items { weight = 2, templateId = 1, userId = 1 },
        new Template_items { weight = 1, templateId = 1, userId = 2 },
        new Template_items { weight = 1, templateId = 1, userId = 3 },
        new Template_items { weight = 1, templateId = 2, userId = 1 },
        new Template_items { weight = 1, templateId = 2, userId = 3 }
    };

        Template_items.AddRange(templateItems);

        SaveChanges();

        Database.CommitTransaction();
    }
}