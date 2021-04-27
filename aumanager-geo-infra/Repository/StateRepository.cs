using aumanager_geo_core.Models;
using aumanager_geo_infra.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aumanager_geo_infra.Repository
{
    public class StateRepository
    {
        private GeoContext _context;

        public StateRepository(GeoContext context) 
        {
            _context = context;
        }

        public async Task<List<State>> GetStatesRepository(string? _citiesParam)
        {
            try
            {
                if (_citiesParam.Equals("yes"))
                {
                    return await _context.States.Include(cities => cities.Cities).ToListAsync<State>();
                }
                else
                {
                    return await _context.States.ToListAsync<State>();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<State> GetStateRepository(int _id, string? _citiesParam)
        {
            State state;

            try
            {
                if (_citiesParam.Equals("yes"))
                {
                    state = await _context.States.Include(cities => cities.Cities).Where(state => state.Id == _id).FirstOrDefaultAsync<State>();
                }
                else
                {
                    state = await _context.States.FindAsync(_id);
                }

                if (state == null)
                {
                    return null;
                }

                return state;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<short> PutStateRepository(int _id, State _state)
        {
            if (_id != _state.Id)
            {
                return 1; //bad request
            }

            _context.Entry(_state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(_id))
                {
                    return 2; //not found
                }
                else
                {
                    throw;
                }
            }

            return 3; //no content
        }

        public async Task<bool> PostStateRepository(State _state)
        {
            try
            {
                _context.States.Add(_state);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<short> DeleteStateRepository(int _id)
        {
            try
            {
                var state = await _context.States.FindAsync(_id);

                if (state == null)
                {
                    return 1; //not found
                }

                _context.States.Remove(state);
                await _context.SaveChangesAsync();

                return 2; //success
            }
            catch (Exception)
            {
                return 3; //bad request
            }
        }

        private bool StateExists(int _id)
        {
            return _context.States.Any(e => e.Id == _id);
        }
    }
}
