using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoncertManager.DAL.Entities
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }   //Befogadókapacitás

        // 1 helyszínhez tartozik több koncert
        public ICollection<Concert> Concerts { get; set; } = new List<Concert>();
    }
}
