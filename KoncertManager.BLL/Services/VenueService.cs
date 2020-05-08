using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.BLL.Exceptions;
using KoncertManager.BLL.Interfaces;
using KoncertManager.DAL;
using KoncertManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoncertManager.BLL.Services
{
    public class VenueService : IVenueService
    {
        private readonly ConcertManagerContext _context;

        public VenueService(ConcertManagerContext context)
        {
            _context = context;
        }

        public async Task<Venue> GetVenueAsync(int venueId)
        {
            return await _context.Venues.SingleOrDefaultAsync(v => v.Id == venueId)
                ?? throw new EntityNotFoundException($"Nem található Id={venueId} helyszín!");
        }

        public async Task<IEnumerable<Venue>> GetVenuesAsync()
        {
            return await _context.Venues.ToListAsync();
        }

        public async Task<Venue> InsertVenueAsync(Venue newVenue)
        {
            _context.Venues.Add(newVenue);
            await _context.SaveChangesAsync();
            return newVenue;
        }

        public async Task UpdateVenueAsync(int venueId, Venue updatedVenue)
        {
            updatedVenue.Id = venueId;
            var entry = _context.Attach(updatedVenue);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(await _context.Venues.SingleOrDefaultAsync(v => v.Id == venueId) == null)
                    throw new EntityNotFoundException($"Nem található Id={venueId} helyszín!");
            }
        }

        public async Task DeleteVenueAsync(int venueId)
        {
            _context.Venues.Remove(new Venue {Id = venueId});
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Venues.SingleOrDefaultAsync(v => v.Id == venueId) == null)
                    throw new EntityNotFoundException($"Nem található Id={venueId} helyszín!");
            }
        }
    }
}
