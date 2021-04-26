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
    public class CountryRepository
    {
        private GeoContext _context = new GeoContext();

        public async Task<List<Country>> GetCountriesRepository(string? _statesParam)
        {
            try
            {
                if(_statesParam.Equals("yes"))
                {
                    return await _context.Countries.Include(states => states.States).ToListAsync<Country>();
                }
                else
                {
                    return await _context.Countries.ToListAsync<Country>();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Country> GetCountryRepository(int _id, string? _statesParam)
        {
            Country country;

            try
            {
                if (_statesParam.Equals("yes"))
                {
                    country = await _context.Countries.Include(states => states.States).Where(country => country.Id == _id).FirstOrDefaultAsync<Country>();
                }
                else
                {
                    country = await _context.Countries.FindAsync(_id);
                }

                if (country == null)
                {
                    return null;
                }

                return country;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<short> PutCountryRepository(int _id, Country _country)
        {
            if (_id != _country.Id)
            {
                return 1; //bad request
            }

            _context.Entry(_country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(_id))
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

        public async Task<bool> PostCountryRepository(Country _country)
        {
            try
            {
                _context.Countries.Add(_country);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<short> DeleteCountryRepository(int _id)
        {
            try
            {
                var country = await _context.Countries.FindAsync(_id);

                if (country == null)
                {
                    return 1; //not found
                }

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                return 2; //success
            }
            catch (Exception)
            {
                return 3; //bad request
            }
        }

        private bool CountryExists(int _id)
        {
            return _context.Countries.Any(e => e.Id == _id);
        }
    }
}
