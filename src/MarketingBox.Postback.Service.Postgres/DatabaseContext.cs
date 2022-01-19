using MarketingBox.Postback.Service.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;

namespace MarketingBox.Postback.Service.Postgres
{
    public class DatabaseContext : MyDbContext
    {
        public const string Schema = "postback-service";

        private const string ReferenceTableName = "reference";

        public DbSet<ReferenceEntity> References { get; set; }
        
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (LoggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(LoggerFactory).EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            SetReferenceEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetReferenceEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReferenceEntity>().ToTable(ReferenceTableName);
            modelBuilder.Entity<ReferenceEntity>().HasKey(e => e.ReferenceId);
            modelBuilder.Entity<ReferenceEntity>().HasIndex(e => e.AffiliateId).IsUnique();
        }
    }
}