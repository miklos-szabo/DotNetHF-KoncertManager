﻿// <auto-generated />
using System;
using KoncertManager.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KoncertManager.DAL.Migrations
{
    [DbContext(typeof(ConcertManagerContext))]
    [Migration("20200612121844_MySqlInit")]
    partial class MySqlInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("KoncertManager.DAL.Entities.Band", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FormedIn")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Bands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "Svédország",
                            FormedIn = 1999,
                            Name = "Sabaton"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Finnország",
                            FormedIn = 2015,
                            Name = "Beast in Black"
                        },
                        new
                        {
                            Id = 3,
                            Country = "Svédország",
                            FormedIn = 1993,
                            Name = "Hammerfall"
                        },
                        new
                        {
                            Id = 4,
                            Country = "Németország",
                            FormedIn = 2003,
                            Name = "Powerwolf"
                        },
                        new
                        {
                            Id = 5,
                            Country = "Finnország",
                            FormedIn = 1996,
                            Name = "Nightwish"
                        },
                        new
                        {
                            Id = 6,
                            Country = "Magyarország",
                            FormedIn = 1999,
                            Name = "Depresszió"
                        });
                });

            modelBuilder.Entity("KoncertManager.DAL.Entities.Concert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("TicketsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VenueId");

                    b.ToTable("Concerts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = false,
                            VenueId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2020, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = true,
                            VenueId = 3
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2020, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = false,
                            VenueId = 2
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2020, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = false,
                            VenueId = 1
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = true,
                            VenueId = 4
                        },
                        new
                        {
                            Id = 6,
                            Date = new DateTime(2020, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = true,
                            VenueId = 1
                        },
                        new
                        {
                            Id = 7,
                            Date = new DateTime(2021, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TicketsAvailable = false,
                            VenueId = 2
                        });
                });

            modelBuilder.Entity("KoncertManager.DAL.Entities.ConcertBand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BandId")
                        .HasColumnType("int");

                    b.Property<int>("ConcertId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BandId");

                    b.HasIndex("ConcertId");

                    b.ToTable("ConcertBand");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BandId = 1,
                            ConcertId = 1
                        },
                        new
                        {
                            Id = 2,
                            BandId = 2,
                            ConcertId = 1
                        },
                        new
                        {
                            Id = 3,
                            BandId = 3,
                            ConcertId = 2
                        },
                        new
                        {
                            Id = 4,
                            BandId = 4,
                            ConcertId = 3
                        },
                        new
                        {
                            Id = 5,
                            BandId = 5,
                            ConcertId = 3
                        },
                        new
                        {
                            Id = 6,
                            BandId = 6,
                            ConcertId = 4
                        },
                        new
                        {
                            Id = 7,
                            BandId = 2,
                            ConcertId = 4
                        },
                        new
                        {
                            Id = 8,
                            BandId = 4,
                            ConcertId = 5
                        },
                        new
                        {
                            Id = 9,
                            BandId = 6,
                            ConcertId = 5
                        },
                        new
                        {
                            Id = 10,
                            BandId = 3,
                            ConcertId = 6
                        },
                        new
                        {
                            Id = 11,
                            BandId = 1,
                            ConcertId = 6
                        });
                });

            modelBuilder.Entity("KoncertManager.DAL.Entities.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Venues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "1117 Budapest, Prielle Kornélia utca 4",
                            Capacity = 1045,
                            Name = "Barba Negra Music Club"
                        },
                        new
                        {
                            Id = 2,
                            Address = "1117 Budapest, Neumann János u. 2",
                            Capacity = 6000,
                            Name = "Barba Negra Track"
                        },
                        new
                        {
                            Id = 3,
                            Address = "1143 Budapest, Stefánia Út 2.",
                            Capacity = 12500,
                            Name = "Papp László Budapest Sportaréna"
                        },
                        new
                        {
                            Id = 4,
                            Address = "1117 Budapest, Petőfi híd",
                            Capacity = 600,
                            Name = "A38 hajó"
                        });
                });

            modelBuilder.Entity("KoncertManager.DAL.Entities.Concert", b =>
                {
                    b.HasOne("KoncertManager.DAL.Entities.Venue", "Venue")
                        .WithMany("Concerts")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("KoncertManager.DAL.Entities.ConcertBand", b =>
                {
                    b.HasOne("KoncertManager.DAL.Entities.Band", "Band")
                        .WithMany("ConcertBands")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KoncertManager.DAL.Entities.Concert", "Concert")
                        .WithMany("ConcertBands")
                        .HasForeignKey("ConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
