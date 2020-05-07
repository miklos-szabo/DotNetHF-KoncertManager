using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KoncertManager.BLL;

namespace KoncertManager.API.AutoMapper
{
    public class KoncertManagerProfile : Profile
    {
        public KoncertManagerProfile()
        {
            //Koncertek leképzése DTO-ra
            CreateMap<DAL.Entities.Concert, BLL.DTOs.Concert>()
                .ForMember(dtoC => dtoC.Bands, opt => opt.Ignore()) //Az együttesek listáját kézileg állítjuk össze, kihagyjuk
                .AfterMap((c, dtoC, context) =>         
                    //A Mapping után összeállítjuk az együttesek listáját
                    //Lekérdezzük az összes ConcertBand elemre a hozzájuk tartozó együttest, ezeket listába tesszük
                    dtoC.Bands = c.ConcertBands.Select(cb => 
                        context.Mapper.Map<BLL.DTOs.Band>(cb.Band)).ToList())   //DTO elemként tároljuk el
                .ReverseMap();

            //Együttesek leképzése
            CreateMap<DAL.Entities.Band, BLL.DTOs.Band>().ReverseMap();

            //Helyszínek leképzése
            CreateMap<DAL.Entities.Venue, BLL.DTOs.Venue>().ReverseMap();
        }
    }
}
