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
    public class ConcertsController : ODataController
    {
        private readonly IConcertService _concertService;
        private readonly IMapper _mapper;

        public ConcertsController(IConcertService concertService, IMapper mapper)
        {
            _concertService = concertService;
            _mapper = mapper;
        }

        // GET: api/Concerts
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IQueryable<Concert>>> GetAsync()
        {
            //Lekérjük a listát, és a DAL elemekből DTO-t csinálunk, amiből pedig Queryable-t csinálunk az OData-hoz
            return Ok(_mapper.Map<List<Concert>>(await _concertService.GetConcertsAsync()).AsQueryable());
        }

        // GET: api/Concerts/5
        [EnableQuery]
        [HttpGet("{key}", Name = "GetConcert")]
        public async Task<ActionResult<IQueryable<Concert>>> Get([FromODataUri] int key)    //Nem lehet GetAsync a neve, létrehozáskor 500 lenne
        {
            //Lekérjük az elemet, és a DAL elemből DTO-t csinálunk, amiből pedig Queryable-t csinálunk az OData-hoz
            var result = _mapper.Map<Concert>(await _concertService.GetConcertAsync(key)).ToQueryable();
            return Ok(SingleResult.Create(result));
        }

        // POST: api/Concerts
        [HttpPost]
        public async Task<ActionResult<Concert>> PostAsync([FromBody] Concert concert)
        {
            //Az együttes DTO elemből DAL elemet csinálunk, és beillesztjük az adatbázisba
            var created = await _concertService.InsertConcertAsync(_mapper.Map<DAL.Entities.Concert>(concert),
                _mapper.Map<List<DAL.Entities.Band>>(concert.Bands));

            //201-es kódot adunk vissza
            return CreatedAtAction(
                nameof(Get),        //Csak Get lehet a függvény neve, ha GetAsync, 500-at kap a kliens
                new {id = created.Id},
                _mapper.Map<Concert>(created));
        }

        // PUT: api/Concerts/5
        [HttpPut("{key}")]
        public async Task<ActionResult> PutAsync([FromODataUri] int key, [FromBody] Concert concert)
        {
            //Frissítjük az adott ID-jű elemet a kapott elem szerint
            await _concertService.UpdateConcertAsync(key, _mapper.Map<DAL.Entities.Concert>(concert),
                _mapper.Map<List<DAL.Entities.Band>>(concert.Bands));
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{key}")]
        public async Task<ActionResult> Delete([FromODataUri] int key)
        {
            await _concertService.DeleteConcertAsync(key);
            return NoContent();
        }
    }
}
