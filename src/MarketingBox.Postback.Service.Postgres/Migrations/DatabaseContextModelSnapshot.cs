﻿// <auto-generated />
using System;
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
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarketingBox.Postback.Service.Domain.Models.EventReferenceLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AffiliateId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventMessage")
                        .HasColumnType("text");

                    b.Property<int>("EventType")
                        .HasColumnType("integer");

                    b.Property<int>("HttpQueryType")
                        .HasColumnType("integer");

                    b.Property<string>("PostbackReference")
                        .HasColumnType("text");

                    b.Property<string>("PostbackResponse")
                        .HasColumnType("text");

                    b.Property<int>("PostbackResponseStatus")
                        .HasColumnType("integer");

                    b.Property<string>("RegistrationUId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AffiliateId");

                    b.HasIndex("Date");

                    b.HasIndex("EventType");

                    b.HasIndex("HttpQueryType");

                    b.HasIndex("PostbackResponseStatus");

                    b.ToTable("eventreferencelog", "postback-service");
                });

            modelBuilder.Entity("MarketingBox.Postback.Service.Domain.Models.Reference", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AffiliateId")
                        .HasColumnType("bigint");

                    b.Property<string>("DepositReference")
                        .HasColumnType("text");

                    b.Property<string>("DepositTGReference")
                        .HasColumnType("text");

                    b.Property<int>("HttpQueryType")
                        .HasColumnType("integer");

                    b.Property<string>("RegistrationReference")
                        .HasColumnType("text");

                    b.Property<string>("RegistrationTGReference")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AffiliateId")
                        .IsUnique();

                    b.ToTable("reference", "postback-service");
                });

            modelBuilder.Entity("MarketingBox.Postback.Service.Postgres.Entities.AffiliateReferenceLogEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AffiliateId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Operation")
                        .HasColumnType("integer");

                    b.Property<long?>("ReferenceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AffiliateId");

                    b.HasIndex("Date");

                    b.HasIndex("Operation");

                    b.ToTable("affiliatereferencelog", "postback-service");
                });
#pragma warning restore 612, 618
        }
    }
}
