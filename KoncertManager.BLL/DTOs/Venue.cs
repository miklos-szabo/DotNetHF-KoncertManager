using System;
using System.Collections.Generic;
using System.Text;

namespace KoncertManager.BLL.DTOs
{
    //Ez egy DTO a kliensoldal számára, csak azokat az adatokat tartalmazza, melyek szükségesek neki
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }
}
