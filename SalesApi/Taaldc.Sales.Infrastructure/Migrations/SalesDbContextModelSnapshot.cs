﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taaldc.Sales.Infrastructure;

#nullable disable

namespace Taaldc.Sales.Infrastructure.Migrations
{
    [DbContext(typeof(SalesDbContext))]
    partial class SalesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("buyerseq", "sales")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderseq", "sales")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderstatusseq", "sales")
                .IncrementsBy(10);

            modelBuilder.HasSequence("paymentseq", "sales")
                .IncrementsBy(10);

            modelBuilder.HasSequence("paymentstatusseq", "sales")
                .IncrementsBy(10);

            modelBuilder.HasSequence("paymentypeseq", "sales")
                .IncrementsBy(10);

            modelBuilder.HasSequence("transactiontypeseq", "sales")
                .IncrementsBy(10);

            modelBuilder.HasSequence("unitreplicaseq", "sales")
                .IncrementsBy(10);

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "buyerseq", "sales");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salutation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TownCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactNo")
                        .IsUnique();

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("buyer", "sales");
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderseq", "sales");

                    b.Property<string>("Broker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRefundable")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("_buyerId")
                        .HasColumnType("int")
                        .HasColumnName("BuyerId");

                    b.Property<int>("_statusId")
                        .HasColumnType("int")
                        .HasColumnName("StatusId");

                    b.Property<int>("_unitId")
                        .HasColumnType("int")
                        .HasColumnName("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("_buyerId");

                    b.HasIndex("_statusId");

                    b.HasIndex("_unitId");

                    b.ToTable("order", "sales");
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderstatusseq", "sales");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("orderstatus", "sales");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "FullyPaid"
                        },
                        new
                        {
                            Id = 2,
                            Name = "PartiallyPaid"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cancelled"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Reserved"
                        },
                        new
                        {
                            Id = 5,
                            Name = "New"
                        });
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "paymentseq", "sales");

                    b.Property<DateTime>("ActualPaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ConfirmationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CorrelationId")
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

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("_paymentTypeId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentTypeId");

                    b.Property<int>("_statusId")
                        .HasColumnType("int")
                        .HasColumnName("StatusId");

                    b.Property<int>("_transactionTypeId")
                        .HasColumnType("int")
                        .HasColumnName("TransactionTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ConfirmationNumber")
                        .IsUnique()
                        .HasDatabaseName("IX_ConfirmationNumber");

                    b.HasIndex("OrderId");

                    b.HasIndex("_paymentTypeId");

                    b.HasIndex("_statusId");

                    b.HasIndex("_transactionTypeId");

                    b.ToTable("payment", (string)null);
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.PaymentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "paymentstatusseq", "sales");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("paymentstatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Void"
                        });
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "paymentypeseq", "sales");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("paymenttype", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Reservation"
                        },
                        new
                        {
                            Id = 2,
                            Name = "PartialDownPayment"
                        },
                        new
                        {
                            Id = 3,
                            Name = "FullPayment"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Penalty"
                        });
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "transactiontypeseq", "sales");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("transactiontype", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ForReservation"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ForAcquisition"
                        });
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.UnitReplica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "unitreplicaseq", "sales");

                    b.Property<double>("BalconyArea")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Floor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Property")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("ScenicView")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScenicViewId")
                        .HasColumnType("int");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Tower")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TowerId")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("UnitArea")
                        .HasColumnType("float");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<string>("UnitStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitStatusId")
                        .HasColumnType("int");

                    b.Property<string>("UnitType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UnitTypeShortCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("unitreplica", "sales");
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Order", b =>
                {
                    b.HasOne("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Buyer", null)
                        .WithMany()
                        .HasForeignKey("_buyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("_statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.UnitReplica", null)
                        .WithMany()
                        .HasForeignKey("_unitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Payment", b =>
                {
                    b.HasOne("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Order", null)
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("_paymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.PaymentStatus", "Status")
                        .WithMany()
                        .HasForeignKey("_statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("_transactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentType");

                    b.Navigation("Status");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate.Order", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
