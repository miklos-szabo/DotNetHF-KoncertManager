using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KoncertManager.BLL.DTOs;
using KoncertManager.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace KoncertManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IVenueService _venueService;
        private readonly IMapper _mapper;

        public VenuesController(IVenueService venueService, IMapper mapper)
        {
            _venueService = venueService;
            _mapper = mapper;
        }

        // GET: api/Venues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetAsync()
        {
            //Lekérjük a listát, és a DAL elemekből DTO-t csinálunk
            return _mapper.Map<List<Venue>>(await _venueService.GetVenuesAsync());
        }

        // GET: api/Venues/5
        [HttpGet("{id}", Name = "GetVenue")]
        public async Task<ActionResult<Venue>> Get(int id)
        {
            //Lekérjük az elemet, és a DAL elemből DTO-t csinálunk
            return _mapper.Map<Venue>(await _venueService.GetVenueAsync(id));
        }

        // POST: api/Venues
        [HttpPost]
        public async Task<ActionResult<Venue>> PostAsync([FromBody] Venue venue)
        {
            //A helyszín DTO elemből DAL elemet csinálunk, és beillesztjük az adatbázisba
            var created = await _venueService.InsertVenueAsync(_mapper.Map<DAL.Entities.Venue>(venue));

            //201-es kódot adunk vissza
            return CreatedAtAction(
                nameof(Get),
                new {id = created.Id},
                _mapper.Map<Venue>(created));
        }

        // PUT: api/Venues/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Venue venue)
        {
            //Frissítjük az adott ID-jű elemet a kapott elem szerint
            await _venueService.UpdateVenueAsync(id, _mapper.Map<DAL.Entities.Venue>(venue));
            return NoContent();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            //Töröljük az adott ID-jű elemet
            await _venueService.DeleteVenueAsync(id);
            return NoContent();
        }
    }
}
