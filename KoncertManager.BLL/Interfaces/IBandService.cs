using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KoncertManager.DAL.Entities;


namespace KoncertManager.BLL.Interfaces
{
    public interface IBandService
    {
        //Visszaad egy együttest ID alapján
        Task<Band> GetBandAsync(int bandId);

        //Visszaadja az összes együttest
        Task<IEnumerable<Band>> GetBandsAsync();

        //Betesz az adatbázisba egy új, paraméterként kapott együttest
        Task<Band> InsertBandAsync(Band newBand);

        //Módosítja az adott ID-jú együttest a kapott együttesre
        Task UpdateBandAsync(int bandId, Band updatedBand);

        //Töröl egy együttest ID alapján
        Task DeleteBandAsync(int bandId);
    }
}
