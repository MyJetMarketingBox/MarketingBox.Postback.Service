﻿// <auto-generated />
using MarketingBox.Postback.Service.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("postback-service")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarketingBox.Postback.Service.Postgres.Entities.ReferenceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AffiliateId")
                        .HasColumnType("bigint");

                    b.Property<int>("CallType")
                        .HasColumnType("integer");

                    b.Property<string>("DepositReference")
                        .HasColumnType("text");

                    b.Property<string>("DepositTGReference")
                        .HasColumnType("text");

                    b.Property<string>("RegistrationReference")
                        .HasColumnType("text");

                    b.Property<string>("RegistrationTGReference")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AffiliateId")
                        .IsUnique();

                    b.ToTable("reference", "postback-service");
                });
#pragma warning restore 612, 618
        }
    }
}
