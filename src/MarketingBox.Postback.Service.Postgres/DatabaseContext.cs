using MarketingBox.Postback.Service.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;

namespace MarketingBox.Postback.Service.Postgres
{
    public class DatabaseContext : MyDbContext
    {
        public const string Schema = "postback-service";

        private const string ReferenceTableName = "reference";
        private const string AffiliateReferenceLogTableName = "affiliatereferencelog";
        private const string EventReferenceLogTableName = "eventreferencelog";

        public DbSet<ReferenceEntity> References { get; set; }
        public DbSet<AffiliateReferenceLogEntity> AffiliateReferenceLogs { get; set; }
        public DbSet<EventReferenceLogEntity> EventReferenceLogs { get; set; }
        
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
            SetAffiliateReferenceLogEntity(modelBuilder);
            SetEventReferenceLogEntity(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SetReferenceEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReferenceEntity>().ToTable(ReferenceTableName);
            modelBuilder.Entity<ReferenceEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<ReferenceEntity>().HasIndex(e => e.AffiliateId).IsUnique();
        }
        private static void SetAffiliateReferenceLogEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AffiliateReferenceLogEntity>().ToTable(AffiliateReferenceLogTableName);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasIndex(e => e.AffiliateId);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasIndex(e => e.Operation);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasIndex(e => e.Date);
        }
        private static void SetEventReferenceLogEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventReferenceLogEntity>().ToTable(EventReferenceLogTableName);
            modelBuilder.Entity<EventReferenceLogEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<EventReferenceLogEntity>().HasIndex(e => e.EventStatus);
            modelBuilder.Entity<EventReferenceLogEntity>().HasIndex(e => e.HttpQueryType);
            modelBuilder.Entity<EventReferenceLogEntity>().HasIndex(e => e.Date);
        }
    }
}