using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PivotIt.Core.Entities;
using PivotIt.Infrastructure.Entities;
using PivotIt.Infrastructure.Models;
using System.Linq;

namespace PivotIt.Infrastructure.Persistence
{
    public class SiteContext : DbContext
    {
        private readonly DatabaseOptions _databaseOptions;

        public DbSet<SiteUser> SiteUser { get; set; }

        public DbSet<AppLog> AppLog { get; set; }

        public DbSet<UserMessage> UserMessage { get; set; }

        public DbSet<UserMessageAttachment> UserMessageAttachment { get; set; }



        public SiteContext(IOptionsMonitor<DatabaseOptions> databaseOptions) : base()
        {
            _databaseOptions = databaseOptions.CurrentValue;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_databaseOptions.SiteConnectionString, o => o.MigrationsAssembly("PivotIt.Infrastructure"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName($"PivotIt_{entityType.GetTableName()}");
                var idProperty = entityType.GetProperties().FirstOrDefault(x => x.Name == "ID");
                if (idProperty != null)
                {
                    var entity = modelBuilder.Entity(entityType.ClrType);
                    modelBuilder.HasSequence<int>($"sq_{entityType.GetTableName()}_ID").StartsAt(1).IncrementsBy(1);
                    entity.Property("ID").HasDefaultValueSql($"NEXT VALUE FOR sq_{entityType.GetTableName()}_ID");
                }
            }

            AddIndexes(modelBuilder);
        }

        private void AddIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppLog>().HasIndex(e => e.LogDate);

            modelBuilder.Entity<SiteUser>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<SiteUser>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<UserMessage>().HasIndex(m => m.UserID);

            modelBuilder.Entity<UserMessageAttachment>().HasIndex(a => a.MessageID);
        }
    }
}
