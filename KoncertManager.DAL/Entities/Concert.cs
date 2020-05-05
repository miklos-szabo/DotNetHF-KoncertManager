using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoncertManager.DAL.Entities
{
    public class Concert
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool TicketsAvailable { get; set; }

        //Melyik helyszínen van a koncert
        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        // Egy koncerten több együttes is fellép
        public ICollection<ConcertBand> ConcertBands { get; set; } = new List<ConcertBand>();
    }
}