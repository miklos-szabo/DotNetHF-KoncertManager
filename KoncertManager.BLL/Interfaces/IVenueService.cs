using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.DAL.Entities;

namespace KoncertManager.BLL.Interfaces
{
    public interface IVenueService
    {
        //Visszaad egy helyszínt ID alapján
        Task<Venue> GetVenueAsync(int venueId);

        //Visszaadja az összes helyszínt
        Task<IEnumerable<Venue>> GetVenuesAsync();

        //Betesz az adatbázisba egy új, paraméterként kapott helyszínt
        Task<Venue> InsertVenueAsync(Venue newVenue);

        //Módosítja az adott ID-jú helyszínt a kapott helyszínre
        Task UpdateVenueAsync(int venueId, Venue updatedVenue);

        //Töröl egy helyszínt ID alapján
        Task DeleteVenueAsync(int venueId);
    }
}
