using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.BLL;
using KoncertManager.BLL.Services;
using KoncertManager.DAL;
using KoncertManager.DAL.Entities;
using Xunit;

namespace UnitTests
{
    public class ServiceTests
    {
        #region BandService

         [Fact]
        public async void CanGetBands()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new BandService(context);
                    List<Band> bands = (await service.GetBandsAsync()).ToList();
                    Assert.Equal(7, bands.Count);
                    Assert.Equal("Sabaton", bands[0].Name);
                    Assert.Equal("Beast in Black", bands[1].Name);
                    Assert.Equal("Hammerfall", bands[2].Name);
                    Assert.Equal("Powerwolf", bands[3].Name);
                    Assert.Equal("Nightwish", bands[4].Name);
                    Assert.Equal("Depresszió", bands[5].Name);
                    Assert.Equal("UnitTest", bands[6].Name);
                }
            }
        }

        [Fact]
        public async void CanGetBand()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new BandService(context);
                    Band band = await service.GetBandAsync(1);
                    Assert.Equal("Sabaton", band.Name);
                }
            }
        }

        [Fact]
        public async void CanCreateBand()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new BandService(context);
                    await service.InsertBandAsync(new Band {Name = "Teszt", Country = "Magyarország", FormedIn = 2020});
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(8, context.Bands.Count());
                    Assert.Equal("Teszt", context.Bands.Find(8).Name);
                }
            }
        }

        [Fact]
        public async void CanUpdateBand()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new BandService(context);
                    await service.UpdateBandAsync(1, new Band {Name = "Teszt", Country = "Magyarország", FormedIn = 2020});
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(7, context.Bands.Count());
                    Assert.Equal("Teszt", context.Bands.Find(1).Name);
                }
            }
        }
        
        [Fact]
        public async void CanDeleteBand()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new BandService(context);
                    await service.DeleteBandAsync(7);
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(6, context.Bands.Count());
                }
            }
        }

        #endregion
        
        #region VenueService

         [Fact]
        public async void CanGetVenues()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new VenueService(context);
                    List<Venue> venues = (await service.GetVenuesAsync()).ToList();
                    Assert.Equal(5, venues.Count);
                    Assert.Equal("Barba Negra Music Club", venues[0].Name);
                    Assert.Equal("Barba Negra Track", venues[1].Name);
                    Assert.Equal("Papp László Budapest Sportaréna", venues[2].Name);
                    Assert.Equal("A38 Hajó", venues[3].Name);
                    Assert.Equal("UnitTest", venues[4].Name);
                }
            }
        }

        [Fact]
        public async void CanGetVenue()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new VenueService(context);
                    Venue venue = await service.GetVenueAsync(1);
                    Assert.Equal("Barba Negra Music Club", venue.Name);
                }
            }
        }

        [Fact]
        public async void CanCreateVenue()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new VenueService(context);
                    await service.InsertVenueAsync(new Venue {Name = "Teszt", Address = "Random", Capacity = 2020});
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(6, context.Venues.Count());
                    Assert.Equal("Teszt", context.Venues.Find(6).Name);
                }
            }
        }

        [Fact]
        public async void CanUpdateVenue()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new VenueService(context);
                    await service.UpdateVenueAsync(1, new Venue {Name = "Teszt", Address = "Magyarország", Capacity = 2020});
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(5, context.Venues.Count());
                    Assert.Equal("Teszt", context.Venues.Find(1).Name);
                }
            }
        }
        
        [Fact]
        public async void CanDeleteVenue()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new VenueService(context);
                    await service.DeleteVenueAsync(5);
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(4, context.Venues.Count());
                }
            }
        }

        #endregion

        #region ConcertService

         [Fact]
        public async void CanGetConcerts()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new ConcertService(context);
                    List<Concert> concerts = (await service.GetConcertsAsync()).ToList();
                    Assert.Equal(7, concerts.Count);
                    Assert.Equal(new DateTime(2019, 03, 04), concerts[0].Date);
                    Assert.Equal(new DateTime(2020, 12, 07), concerts[1].Date);
                    Assert.Equal(new DateTime(2020, 05, 15), concerts[2].Date);
                    Assert.Equal(new DateTime(2020, 02, 27), concerts[3].Date);
                    Assert.Equal(new DateTime(2021, 03, 01), concerts[4].Date);
                    Assert.Equal(new DateTime(2020, 08, 30), concerts[5].Date);
                    Assert.Equal(new DateTime(2021, 01, 18), concerts[6].Date);
                }
            }
        }

        [Fact]
        public async void CanGetConcert()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new ConcertService(context);
                    Concert concert = await service.GetConcertAsync(1);
                    Assert.Equal(new DateTime(2019, 03, 04), concert.Date);
                }
            }
        }

        [Fact]
        public async void CanCreateConcert()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new ConcertService(context);
                    await service.InsertConcertAsync(
                        new Concert {Date = new DateTime(2030,03,03), TicketsAvailable = true, Venue = context.Venues.First()},
                        Enumerable.Empty<Band>().ToList());
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(8, context.Concerts.Count());
                    Assert.Equal(new DateTime(2030,03,03), context.Concerts.Find(8).Date);
                }
            }
        }

        [Fact]
        public async void CanUpdateConcert()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new ConcertService(context);
                    await service.UpdateConcertAsync(1, 
                        new Concert {Date = new DateTime(2030,03,03), TicketsAvailable = true, Venue = context.Venues.First()},
                        Enumerable.Empty<Band>().ToList());
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(7, context.Concerts.Count());
                    Assert.Equal(new DateTime(2030,03,03), context.Concerts.Find(1).Date);
                }
            }
        }
        
        [Fact]
        public async void CanDeleteConcert()
        {
            using (var setup = new SQLiteInMemorySetup())
            {
                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    context.Database.EnsureCreated();
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    var service = new ConcertService(context);
                    await service.DeleteConcertAsync(7);
                }

                using (var context = new ConcertManagerContext(setup.ContextOptions))
                {
                    Assert.Equal(6, context.Concerts.Count());
                }
            }
        }

        #endregion
    }
}
