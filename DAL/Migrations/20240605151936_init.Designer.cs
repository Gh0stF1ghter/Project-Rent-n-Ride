﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AgencyDbContext))]
    [Migration("20240605151936_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("DAL.Entities.Manufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("DAL.Entities.Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ManufacturerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("DAL.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("FuelType")
                        .HasColumnType("integer");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uuid");

                    b.Property<int>("Odo")
                        .HasColumnType("integer");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("RentCost")
                        .HasColumnType("double precision");

                    b.Property<int>("VehicleState")
                        .HasColumnType("integer");

                    b.Property<int>("VehicleType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("DAL.Entities.VehicleClientHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleClientHistories");
                });

            modelBuilder.Entity("DAL.Entities.Client", b =>
                {
                    b.HasOne("DAL.Entities.Vehicle", "Vehicle")
                        .WithOne("Client")
                        .HasForeignKey("DAL.Entities.Client", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DAL.Entities.Model", b =>
                {
                    b.HasOne("DAL.Entities.Manufacturer", "Manufacturer")
                        .WithMany("Models")
                        .HasForeignKey("ManufacturerId");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("DAL.Entities.Vehicle", b =>
                {
                    b.HasOne("DAL.Entities.Model", "Model")
                        .WithMany("Vehicles")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("DAL.Entities.VehicleClientHistory", b =>
                {
                    b.HasOne("DAL.Entities.Client", "Client")
                        .WithMany("VehicleClientHistory")
                        .HasForeignKey("ClientId");

                    b.HasOne("DAL.Entities.Vehicle", "Vehicle")
                        .WithMany("VehicleClientHistory")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Client");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DAL.Entities.Client", b =>
                {
                    b.Navigation("VehicleClientHistory");
                });

            modelBuilder.Entity("DAL.Entities.Manufacturer", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("DAL.Entities.Model", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("DAL.Entities.Vehicle", b =>
                {
                    b.Navigation("Client");

                    b.Navigation("VehicleClientHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
