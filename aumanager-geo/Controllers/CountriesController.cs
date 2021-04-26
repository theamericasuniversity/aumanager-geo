using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using aumanager_geo_infra.Repository;
using aumanager_geo_core.Models;

namespace aumanager_geo_api.Controllers
{
    [Authorize]
    [Route("api/geo/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly CountryRepository _repo = new CountryRepository();

        // GET: api/geo/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            //Parameters verification
            string statesParam = HttpContext.Request.Query["states"].ToString();

            var countries = await _repo.GetCountriesRepository(statesParam);
            
            if(countries == null)
            {
                return BadRequest();
            }
            else
            {
                return countries;
            }
        }

        // GET: api/geo/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            try
            {
                //Parameters verification
                string statesParam = HttpContext.Request.Query["states"].ToString();

                var country = _repo.GetCountryRepository(id, statesParam);
                
                if(country == null)
                {
                    return NotFound();
                }
                else
                {
                    return await country;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
             
        }

        // PUT: api/geo/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            var statusReturned = await _repo.PutCountryRepository(id, country);

            if(statusReturned.Equals(2))
            {
                return NotFound();
            }
            else if(statusReturned.Equals(3))
            {
                return NoContent(); //success
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/geo/Countries
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            bool statusAdd = await _repo.PostCountryRepository(country);

            if(!statusAdd)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/geo/Countries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            short statusDelete = await _repo.DeleteCountryRepository(id);
            var country = await _repo.GetCountryRepository(id, null);

            if (statusDelete.Equals(1))
            {
                return NotFound();
            }
            else if (statusDelete.Equals(2))
            {
                return country;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
