using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KoncertManager.BLL.DTOs;
using KoncertManager.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoncertManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly IBandService _bandService;
        private readonly IMapper _mapper;

        public BandsController(IBandService bandService, IMapper mapper)
        {
            _bandService = bandService;
            _mapper = mapper;
        }

        // GET: api/Bands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Band>>> GetAsync()
        {
            //Lekérjük a listát, és a DAL elemekből DTO-t csinálunk
            return _mapper.Map<List<Band>>(await _bandService.GetBandsAsync());
        }

        // GET: api/Bands/5
        [HttpGet("{id}", Name = "GetBand")]
        public async Task<Band> Get(int id)
        {
            //Lekérjük az elemet, és a DAL elemből DTO-t csinálunk
            return _mapper.Map<Band>(await _bandService.GetBandAsync(id));
        }

        // POST: api/Bands
        [HttpPost]
        public async Task<ActionResult<Band>> PostAsync([FromBody] Band band)
        {
            //Az együttes DTO elemből DAL elemet csinálunk, és beillesztjük az adatbázisba
            var created = await _bandService.InsertBandAsync(_mapper.Map<DAL.Entities.Band>(band));

            //201-es kódot adunk vissza
            return CreatedAtAction(
                nameof(Get),
                new {id = created.Id},
                _mapper.Map<Band>(created));
        }

        // PUT: api/Bands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Band band)
        {
            //Frissítjük az adott ID-jű elemet a kapott elem szerint
            await _bandService.UpdateBandAsync(id, _mapper.Map<DAL.Entities.Band>(band));
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            //Töröljük az adott ID-jű elemet
            await _bandService.DeleteBandAsync(id);
            return NoContent();
        }
    }
}
