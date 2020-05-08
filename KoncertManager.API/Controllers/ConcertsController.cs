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
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService _concertService;
        private readonly IMapper _mapper;

        public ConcertsController(IConcertService concertService, IMapper mapper)
        {
            _concertService = concertService;
            _mapper = mapper;
        }

        // GET: api/Concerts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concert>>> GetAsync()
        {
            //Lekérjük a listát, és a DAL elemekből DTO-t csinálunk
            return _mapper.Map<List<Concert>>(await _concertService.GetConcertsAsync());
        }

        // GET: api/Concerts/5
        [HttpGet("{id}", Name = "GetConcert")]
        public async Task<ActionResult<Concert>> GetAsync(int id)
        {
            //Lekérjük az elemet, és a DAL elemből DTO-t csinálunk
            return _mapper.Map<Concert>(await _concertService.GetConcertAsync(id));
        }

        // POST: api/Concerts
        [HttpPost]
        public async Task<ActionResult<Concert>> PostAsync([FromBody] Concert concert)
        {
            //Az együttes DTO elemből DAL elemet csinálunk, és beillesztjük az adatbázisba
            var created = await _concertService.InsertConcertAsync(_mapper.Map<DAL.Entities.Concert>(concert));

            //201-es kódot adunk vissza
            return CreatedAtAction(
                nameof(GetAsync),
                new {id = concert.Id},
                _mapper.Map<Concert>(concert));
        }

        // PUT: api/Concerts/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Concert concert)
        {
            //Frissítjük az adott ID-jű elemet a kapott elem szerint
            await _concertService.UpdateConcertAsync(id, _mapper.Map<DAL.Entities.Concert>(concert));
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _concertService.DeleteConcertAsync(id);
            return NoContent();
        }
    }
}
