﻿// <auto-generated />
using System;
using MarketingBox.Postback.Service.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketingBox.Postback.Service.Postgres.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220125150224_LogTables")]
    partial class LogTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("postback-service")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.HasKey("Id");

                    b.HasIndex("AffiliateId");

                    b.HasIndex("Date");

                    b.HasIndex("Operation");

                    b.ToTable("affiliatereferencelog", "postback-service");
                });

            modelBuilder.Entity("MarketingBox.Postback.Service.Postgres.Entities.EventReferenceLogEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EventStatus")
                        .HasColumnType("integer");

                    b.Property<int>("HttpQueryType")
                        .HasColumnType("integer");

                    b.Property<string>("PostbackReference")
                        .HasColumnType("text");

                    b.Property<string>("PostbackResult")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("EventStatus");

                    b.HasIndex("HttpQueryType");

                    b.ToTable("eventreferencelog", "postback-service");
                });

            modelBuilder.Entity("MarketingBox.Postback.Service.Postgres.Entities.ReferenceEntity", b =>
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
#pragma warning restore 612, 618
        }
    }
}
