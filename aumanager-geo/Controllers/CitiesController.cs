using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aumanager_geo_core.Models;
using aumanager_geo_infra.Configuration;
using aumanager_geo_infra.Repository;

namespace aumanager_geo.Controllers
{
    [Route("api/geo/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CityRepository _repo;

        public CitiesController(CityRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _repo.GetCitiesRepository();

            if (cities == null)
            {
                return BadRequest();
            }
            else
            {
                return cities;
            }
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            try
            {
                var cities = _repo.GetCityRepository(id);

                if (cities == null)
                {
                    return NotFound();
                }
                else
                {
                    return await cities;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            var statusReturned = await _repo.PutCityRepository(id, city);

            if (statusReturned.Equals(2))
            {
                return NotFound();
            }
            else if (statusReturned.Equals(3))
            {
                return NoContent(); //success
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Cities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            bool statusAdd = await _repo.PostCityRepository(city);

            if (!statusAdd)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _repo.GetCityRepository(id);
            short statusDelete = await _repo.DeleteCityRepository(id);

            if (statusDelete.Equals(1))
            {
                return NotFound();
            }
            else if (statusDelete.Equals(2))
            {
                return city;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
