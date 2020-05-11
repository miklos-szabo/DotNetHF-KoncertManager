using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.DAL.Entities;

namespace KoncertManager.BLL.Interfaces
{
    public interface IConcertService
    {
        //Visszaad egy koncertet ID alapján
        Task<Concert> GetConcertAsync(int concertId);

        //Visszaadja az összes koncertet
        Task<IEnumerable<Concert>> GetConcertsAsync();

        //Betesz az adatbázisba egy új, paraméterként kapott koncertet
        Task<Concert> InsertConcertAsync(Concert newConcert, List<Band> bands);

        //Módosítja az adott ID-jú koncertet a kapott koncertre
        Task UpdateConcertAsync(int concertId, Concert updatedConcert, List<Band> bands);

        //Töröl egy koncertet ID alapján
        Task DeleteConcertAsync(int concertId);
    }
}
