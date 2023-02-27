﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taaldc.Catalog.Infrastructure;

#nullable disable

namespace Taaldc.Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20230227034004_fixed-warnings")]
    partial class fixedwarnings
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("floorseq", "catalog")
                .StartsAt(2000L);

            modelBuilder.HasSequence<int>("projectseq", "catalog")
                .StartsAt(2000L);

            modelBuilder.HasSequence<int>("propertyseq", "catalog")
                .StartsAt(2000L);

            modelBuilder.HasSequence("scenicviewseq", "catalog")
                .IncrementsBy(10);

            modelBuilder.HasSequence<int>("towerseq", "catalog")
                .StartsAt(2000L);

            modelBuilder.HasSequence<int>("unitseq", "catalog")
                .StartsAt(2000L);

            modelBuilder.HasSequence("unitstatusseq", "catalog")
                .IncrementsBy(10);

            modelBuilder.HasSequence<int>("unittypeseq", "catalog")
                .StartsAt(9L);

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "floorseq", "catalog");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FloorPlanFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TowerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TowerId");

                    b.ToTable("floors", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "projectseq", "catalog");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("project", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "propertyseq", "catalog");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("LandArea")
                        .HasColumnType("float");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProjectId");

                    b.ToTable("property", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Tower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "towerseq", "catalog");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PropertyId");

                    b.ToTable("tower", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "unitseq", "catalog");

                    b.Property<double>("BalconyArea")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("FloorArea")
                        .HasColumnType("float");

                    b.Property<int>("FloorId")
                        .HasColumnType("int");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("_scenicViewId")
                        .HasColumnType("int")
                        .HasColumnName("ScenicViewId");

                    b.Property<int>("_unitStatusId")
                        .HasColumnType("int")
                        .HasColumnName("UnitStatus");

                    b.Property<int>("_unitTypeId")
                        .HasColumnType("int")
                        .HasColumnName("UnitType");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.HasIndex("_scenicViewId");

                    b.HasIndex("_unitStatusId");

                    b.HasIndex("_unitTypeId");

                    b.ToTable("unit", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate.ScenicView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "scenicviewseq", "catalog");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("scenicview", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate.UnitStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "unitstatusseq", "catalog");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("unitstatus", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate.UnitType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "unittypeseq", "catalog");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("unittype", "catalog");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Floor", b =>
                {
                    b.HasOne("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Tower", null)
                        .WithMany("Floors")
                        .HasForeignKey("TowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Property", b =>
                {
                    b.HasOne("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Project", null)
                        .WithMany("Properties")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Tower", b =>
                {
                    b.HasOne("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Property", null)
                        .WithMany("Towers")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Unit", b =>
                {
                    b.HasOne("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Floor", null)
                        .WithMany("Units")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate.ScenicView", null)
                        .WithMany()
                        .HasForeignKey("_scenicViewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate.UnitStatus", null)
                        .WithMany()
                        .HasForeignKey("_unitStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate.UnitType", null)
                        .WithMany()
                        .HasForeignKey("_unitTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Floor", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Project", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Property", b =>
                {
                    b.Navigation("Towers");
                });

            modelBuilder.Entity("Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Tower", b =>
                {
                    b.Navigation("Floors");
                });
#pragma warning restore 612, 618
        }
    }
}
