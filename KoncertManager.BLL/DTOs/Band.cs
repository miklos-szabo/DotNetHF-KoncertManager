using System;
using System.Collections.Generic;
using System.Text;

namespace KoncertManager.BLL.DTOs
{
    //Ez egy DTO a kliensoldal számára, csak azokat az adatokat tartalmazza, melyek szükségesek neki
    public class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FormedIn { get; set; }
        public string Country { get; set; }
    }
}
