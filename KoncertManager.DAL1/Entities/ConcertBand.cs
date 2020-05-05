using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoncertManager.DAL.Entities
{
    /**
     * A több-több kapcsolatot kibontó osztály
     * Egy koncerten fellép több együttes, és egy együttes több koncerten is fellép
     */
    public class ConcertBand
    {
        public int Id { get; set; }

        public int ConcertId { get; set; }
        public Concert Concert { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }
    }
}
