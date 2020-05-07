using System;
using System.Collections.Generic;
using System.Text;

namespace KoncertManager.BLL.DTOs
{
    //Ez egy DTO a kliensoldal számára, csak azokat az adatokat tartalmazza, melyek szükségesek neki
    public class Concert
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool TicketsAvailable { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public List<Band> Bands { get; set; }
    }
}
