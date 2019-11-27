﻿// <auto-generated />
using System;
using InventoryManagementApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Pitstop.InventoryManagementApi.Migrations
{
    [DbContext(typeof(InventoryManagementDbContext))]
    [Migration("20191125205741_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InventoryManagementApi.Models.Inventory", b =>
                {
                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("ProductCode");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("InventoryManagementApi.Models.InventoryUsed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityUsed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InventoryUsed");
                });
#pragma warning restore 612, 618
        }
    }
}