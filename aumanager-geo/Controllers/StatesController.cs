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
using Microsoft.AspNetCore.Authorization;

namespace aumanager_geo.Controllers
{
    [Authorize]
    [Route("api/geo/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly StateRepository _repo;

        public StatesController(StateRepository repo)
        {
            _repo = repo;
        }

        // GET: api/States
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        {
            //Parameters verification
            string citiesParam = HttpContext.Request.Query["cities"].ToString();

            var states = await _repo.GetStatesRepository(citiesParam);

            if (states == null)
            {
                return BadRequest();
            }
            else
            {
                return states;
            }
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            try
            {
                //Parameters verification
                string citiesParam = HttpContext.Request.Query["cities"].ToString();

                var states = _repo.GetStateRepository(id, citiesParam);

                if (states == null)
                {
                    return NotFound();
                }
                else
                {
                    return await states;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // PUT: api/States/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(int id, State state)
        {
            var statusReturned = await _repo.PutStateRepository(id, state);

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

        // POST: api/States
        [HttpPost]
        public async Task<ActionResult<State>> PostState(State state)
        {
            bool statusAdd = await _repo.PostStateRepository(state);

            if (!statusAdd)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetState", new { id = state.Id }, state);
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<State>> DeleteState(int id)
        {
            var state = await _repo.GetStateRepository(id, "no");
            short statusDelete = await _repo.DeleteStateRepository(id);
            
            if (statusDelete.Equals(1))
            {
                return NotFound();
            }
            else if (statusDelete.Equals(2))
            {
                return state;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
