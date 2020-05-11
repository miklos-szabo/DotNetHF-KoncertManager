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
    public class ConcertService : IConcertService
    {
        private readonly ConcertManagerContext _context;

        public ConcertService(ConcertManagerContext context)
        {
            _context = context;
        }

        public async Task<Concert> GetConcertAsync(int concertId)
        {
            return await _context.Concerts
                       .Include(c => c.Venue)
                       .Include(c => c.ConcertBands)
                       .ThenInclude(cb => cb.Band)
                       .SingleOrDefaultAsync(c => c.Id == concertId)
                   ?? throw new EntityNotFoundException($"Nem található Id={concertId} koncert!");
        }

        public async Task<IEnumerable<Concert>> GetConcertsAsync()
        {
            return await _context.Concerts
                .Include(c => c.Venue)
                .Include(c => c.ConcertBands)
                .ThenInclude(cb => cb.Band)
                .ToListAsync();
        }

        public async Task<Concert> InsertConcertAsync(Concert newConcert, List<Band> bands)
        {
            bands.ForEach(b =>
            {
                var concertBand = new ConcertBand{Band = _context.Bands.Find(b.Id), Concert = newConcert};
                newConcert.ConcertBands.Add(concertBand);
            });

            _context.Concerts.Add(newConcert);
            await _context.SaveChangesAsync();
            return newConcert;
        }

        public async Task UpdateConcertAsync(int concertId, Concert updatedConcert, List<Band> bands)
        {
            updatedConcert.Id = concertId;

            bands.ForEach(b =>
            {
                var concertBand = new ConcertBand { Band = _context.Bands.Find(b.Id), Concert = updatedConcert };
                updatedConcert.ConcertBands.Add(concertBand);
            });

            var entry = _context.Concerts.Attach(updatedConcert);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(await _context.Concerts.SingleOrDefaultAsync(c => c.Id == concertId) == null)
                    throw new EntityNotFoundException($"Nem található Id={concertId} koncert!");
            }
        }

        public async Task DeleteConcertAsync(int concertId)
        {
            _context.Concerts.Remove(new Concert {Id = concertId});
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Concerts.SingleOrDefaultAsync(c => c.Id == concertId) == null)
                    throw new EntityNotFoundException($"Nem található Id={concertId} koncert!");
            }
        }
    }
}
