using System;
using System.Collections.Generic;
using KoncertManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoncertManager.DAL
{
    public class ConcertManagerContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Venue> Venues { get; set; }

        public ConcertManagerContext(DbContextOptions<ConcertManagerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Band>()
                .Property(b => b.Name)
                .IsRequired();


            modelBuilder.Entity<Band>().HasData(new List<Band>
            {
                new Band {Id = 1, Name = "Sabaton", FormedIn = 1999, Country = "Svédország"},
                new Band {Id = 2, Name = "Beast in Black", FormedIn = 2015, Country = "Finnország"},
                new Band {Id = 3, Name = "Hammerfall", FormedIn = 1993, Country = "Svédország"},
                new Band {Id = 4, Name = "Powerwolf", FormedIn = 2003, Country = "Németország"},
                new Band {Id = 5, Name = "Nightwish", FormedIn = 1996, Country = "Finnország"},
                new Band {Id = 6, Name = "Depresszió", FormedIn = 1999, Country = "Magyarország"},
                new Band {Id = 7, Name = "UnitTest", FormedIn = 2020, Country = "Magyarország"}
            });

            //Törléskor írjon null-okat azokhoz a koncertekhez
            modelBuilder.Entity<Band>()
                .HasMany(b => b.ConcertBands)
                .WithOne(cb => cb.Band)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venue>()
                .Property(v => v.Name)
                .IsRequired();

            modelBuilder.Entity<Venue>().HasData(new List<Venue>
            {
                new Venue
                {
                    Id = 1, Name = "Barba Negra Music Club",
                    Address = "1117 Budapest, Prielle Kornélia utca 4", Capacity = 1045
                },
                new Venue
                {
                    Id = 2, Name = "Barba Negra Track",
                    Address = "1117 Budapest, Neumann János u. 2", Capacity = 6000
                },
                new Venue
                {
                    Id = 3, Name = "Papp László Budapest Sportaréna",
                    Address = "1143 Budapest, Stefánia Út 2.", Capacity = 12500
                },
                new Venue
                {
                    Id = 4, Name = "A38 Hajó",
                    Address = "1117 Budapest, Petőfi híd", Capacity = 600
                },
                new Venue
                {
                    Id = 5, Name = "UnitTest",
                    Address = "Rider", Capacity = 1000
                }
            });

            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Concerts)
                .WithOne(c => c.Venue)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Concert>().HasData(new List<Concert>
            {
                new Concert {Id = 1, Date = new DateTime(2019, 03, 04), TicketsAvailable = false, VenueId = 1},
                new Concert {Id = 2, Date = new DateTime(2020, 12, 07), TicketsAvailable = true, VenueId = 3},
                new Concert {Id = 3, Date = new DateTime(2020, 05, 15), TicketsAvailable = false, VenueId = 2},
                new Concert {Id = 4, Date = new DateTime(2020, 02, 27), TicketsAvailable = false, VenueId = 1},
                new Concert {Id = 5, Date = new DateTime(2021, 03, 01), TicketsAvailable = true, VenueId = 4},
                new Concert {Id = 6, Date = new DateTime(2020, 08, 30), TicketsAvailable = true, VenueId = 1},
                new Concert {Id = 7, Date = new DateTime(2021, 01, 18), TicketsAvailable = false, VenueId = 2}
            });

            modelBuilder.Entity<ConcertBand>().HasData(new List<ConcertBand>
            {
                new ConcertBand {Id = 1, BandId = 1, ConcertId = 1},
                new ConcertBand {Id = 2, BandId = 2, ConcertId = 1},
                new ConcertBand {Id = 3, BandId = 3, ConcertId = 2},
                new ConcertBand {Id = 4, BandId = 4, ConcertId = 3},
                new ConcertBand {Id = 5, BandId = 5, ConcertId = 3},
                new ConcertBand {Id = 6, BandId = 6, ConcertId = 4},
                new ConcertBand {Id = 7, BandId = 2, ConcertId = 4},
                new ConcertBand {Id = 8, BandId = 4, ConcertId = 5},
                new ConcertBand {Id = 9, BandId = 6, ConcertId = 5},
                new ConcertBand {Id = 10, BandId = 3, ConcertId = 6},
                new ConcertBand {Id = 11, BandId = 1, ConcertId = 6}
            });
        }
    }
}
