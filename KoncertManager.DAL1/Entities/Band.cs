using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoncertManager.DAL.Entities
{
    public class Band
    {
        public int Id {get; set; }
        public string Name { get; set; }
        public int FormedIn { get; set; }
        public string Country { get; set; }

        //Egy együttes több koncerten is fellép
        public ICollection<ConcertBand> ConcertBands { get; } = new List<ConcertBand>();
    }
}
