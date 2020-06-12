using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KoncertManager.BLL.DTOs;
using KoncertManager.BLL.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoncertManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ODataController
    {
        private readonly IBandService _bandService;
        private readonly IMapper _mapper;

        public BandsController(IBandService bandService, IMapper mapper)
        {
            _bandService = bandService;
            _mapper = mapper;
        }

        // GET: api/Bands
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IQueryable<Band>>> GetAsync()
        {
            //Lekérjük a listát, és a DAL elemekből DTO-t csinálunk, amiből pedig Queryable-t csinálunk az OData-hoz
            return Ok(_mapper.Map<List<Band>>(await _bandService.GetBandsAsync()).AsQueryable());
        }

        // GET: api/Bands/5
        [EnableQuery]
        [HttpGet("{key}", Name = "GetBand")]
        public async Task<ActionResult<IQueryable<Band>>> Get([FromODataUri] int key) //Nem lehet GetAsync a neve, létrehozáskor 500 lenne
        {
            //Lekérjük az elemet, és a DAL elemből DTO-t csinálunk, amiből pedig Queryable-t csinálunk az OData-hoz
            var result = _mapper.Map<Band>(await _bandService.GetBandAsync(key)).ToQueryable();
            return Ok(SingleResult.Create(result));
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
        [HttpPut("{key}")]
        public async Task<IActionResult> PutAsync([FromODataUri] int key, [FromBody] Band band)
        {
            //Frissítjük az adott ID-jű elemet a kapott elem szerint
            await _bandService.UpdateBandAsync(key, _mapper.Map<DAL.Entities.Band>(band));
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteAsync([FromODataUri] int key)
        {
            //Töröljük az adott ID-jű elemet
            await _bandService.DeleteBandAsync(key);
            return NoContent();
        }
    }
}
