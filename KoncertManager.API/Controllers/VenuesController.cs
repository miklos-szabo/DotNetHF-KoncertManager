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
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace KoncertManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ODataController
    {
        private readonly IVenueService _venueService;
        private readonly IMapper _mapper;

        public VenuesController(IVenueService venueService, IMapper mapper)
        {
            _venueService = venueService;
            _mapper = mapper;
        }

        // GET: api/Venues
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IQueryable<Venue>>> GetAsync()
        {
            //Lekérjük a listát, és a DAL elemekből DTO-t csinálunk, amiből pedig Queryable-t csinálunk az OData-hoz
            return Ok(_mapper.Map<List<Venue>>(await _venueService.GetVenuesAsync()).AsQueryable());
        }

        // GET: api/Venues/5
        [EnableQuery]
        [HttpGet("{key}", Name = "GetVenue")]
        public async Task<ActionResult<IQueryable<Venue>>> Get([FromODataUri] int key) //Nem lehet GetAsync a neve, létrehozáskor 500 lenne
        {
            //Lekérjük az elemet, és a DAL elemből DTO-t csinálunk, amiből pedig Queryable-t csinálunk az OData-hoz
            var result = _mapper.Map<Venue>(await _venueService.GetVenueAsync(key)).ToQueryable();
            return Ok(SingleResult.Create(result));
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
        [HttpPut("{key}")]
        public async Task<ActionResult> PutAsync([FromODataUri] int key, [FromBody] Venue venue)
        {
            //Frissítjük az adott ID-jű elemet a kapott elem szerint
            await _venueService.UpdateVenueAsync(key, _mapper.Map<DAL.Entities.Venue>(venue));
            return NoContent();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{key}")]
        public async Task<ActionResult> DeleteAsync([FromODataUri] int key)
        {
            //Töröljük az adott ID-jű elemet
            await _venueService.DeleteVenueAsync(key);
            return NoContent();
        }
    }
}
