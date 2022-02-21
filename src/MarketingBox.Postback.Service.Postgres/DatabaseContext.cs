using MarketingBox.Postback.Service.Domain.Models;
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

        public DbSet<Reference> References { get; set; }
        public DbSet<AffiliateReferenceLogEntity> AffiliateReferenceLogs { get; set; }
        public DbSet<EventReferenceLog> EventReferenceLogs { get; set; }
        
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

            SetReference(modelBuilder);
            SetAffiliateReferenceLogEntity(modelBuilder);
            SetEventReferenceLog(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SetReference(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reference>().ToTable(ReferenceTableName);
            modelBuilder.Entity<Reference>().HasKey(e => e.Id);
            modelBuilder.Entity<Reference>().HasIndex(e => e.AffiliateId).IsUnique();
        }

        private static void SetAffiliateReferenceLogEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AffiliateReferenceLogEntity>().ToTable(AffiliateReferenceLogTableName);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasIndex(e => e.AffiliateId);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasIndex(e => e.Operation);
            modelBuilder.Entity<AffiliateReferenceLogEntity>().HasIndex(e => e.Date);
            modelBuilder.Entity<AffiliateReferenceLogEntity>()
                .HasOne<Reference>()
                .WithMany()
                .HasForeignKey(e => e.ReferenceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        

        private static void SetEventReferenceLog(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventReferenceLog>().ToTable(EventReferenceLogTableName);
            modelBuilder.Entity<EventReferenceLog>().HasKey(e => e.Id);
            modelBuilder.Entity<EventReferenceLog>().HasIndex(e => e.AffiliateId);
            modelBuilder.Entity<EventReferenceLog>().HasIndex(e => e.EventType);
            modelBuilder.Entity<EventReferenceLog>().HasIndex(e => e.ResponseStatus);
            modelBuilder.Entity<EventReferenceLog>().HasIndex(e => e.HttpQueryType);
            modelBuilder.Entity<EventReferenceLog>().HasIndex(e => e.Date);
        }
    }
}
