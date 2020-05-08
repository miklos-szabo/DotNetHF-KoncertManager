using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.BLL.Exceptions;
using KoncertManager.BLL.Interfaces;
using KoncertManager.DAL;
using KoncertManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoncertManager.BLL
{
    public class BandService : IBandService
    {
        private readonly ConcertManagerContext _context;

        public BandService(ConcertManagerContext context)
        {
            _context = context;
        }

        public async Task<Band> GetBandAsync(int bandId)
        {
            return await _context.Bands.SingleOrDefaultAsync(b => b.Id == bandId) 
                   ?? throw new EntityNotFoundException($"Nem található Id={bandId} együttes!");
        }

        public async Task<IEnumerable<Band>> GetBandsAsync()
        {
            return await _context.Bands.ToListAsync();
        }

        public async Task<Band> InsertBandAsync(Band newBand)
        { 
            _context.Bands.Add(newBand);
            await _context.SaveChangesAsync();
            return newBand;
        }

        public async Task UpdateBandAsync(int bandId, Band updatedBand)
        {
            updatedBand.Id = bandId;
            var entry = _context.Attach(updatedBand);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(await _context.Bands.SingleOrDefaultAsync(b => b.Id == bandId) == null)
                    throw new EntityNotFoundException($"Nem található Id={bandId} együttes!");
                throw;
            }
        }

        public async Task DeleteBandAsync(int bandId)
        {
            _context.Bands.Remove(new Band {Id = bandId});
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Bands.SingleOrDefaultAsync(b => b.Id == bandId) == null)
                    throw new EntityNotFoundException("Nem található az együttes!");
                throw;
            }
        }
    }
}
