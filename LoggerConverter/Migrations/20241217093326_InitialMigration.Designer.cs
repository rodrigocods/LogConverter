﻿// <auto-generated />
using System;
using LoggerConverter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoggerConverter.Migrations
{
    [DbContext(typeof(LogContext))]
    [Migration("20241217093326_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoggerConverter.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2024, 12, 17, 6, 33, 26, 278, DateTimeKind.Local));

                    b.Property<bool>("IsConverted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2024, 12, 17, 6, 33, 26, 279, DateTimeKind.Local));

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("LoggerConverter.Models.LogConverted", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local));

                    b.Property<int>("IdLog");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local));

                    b.HasKey("Id");

                    b.HasIndex("IdLog")
                        .IsUnique();

                    b.ToTable("LogsConverted");
                });

            modelBuilder.Entity("LoggerConverter.Models.LogConverted", b =>
                {
                    b.HasOne("LoggerConverter.Models.Log", "Log")
                        .WithOne("LogConverted")
                        .HasForeignKey("LoggerConverter.Models.LogConverted", "IdLog")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
