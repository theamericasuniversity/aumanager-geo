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
    public class CityRepository
    {
        private GeoContext _context;

        public CityRepository(GeoContext context) 
        {
            _context = context;
        }

        public async Task<List<City>> GetCitiesRepository()
        {
            try
            {
                return await _context.Cities.ToListAsync<City>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<City> GetCityRepository(int _id)
        {
            City city;

            try
            {
                city = await _context.Cities.FindAsync(_id);

                if (city == null)
                {
                    return null;
                }

                return city;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<short> PutCityRepository(int _id, City _city)
        {
            if (_id != _city.Id)
            {
                return 1; //bad request
            }

            _context.Entry(_city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(_id))
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

        public async Task<bool> PostCityRepository(City _city)
        {
            try
            {
                _context.Cities.Add(_city);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<short> DeleteCityRepository(int _id)
        {
            try
            {
                var city = await _context.Cities.FindAsync(_id);

                if (city == null)
                {
                    return 1; //not found
                }

                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();

                return 2; //success
            }
            catch (Exception)
            {
                return 3; //bad request
            }
        }

        private bool CityExists(int _id)
        {
            return _context.Cities.Any(e => e.Id == _id);
        }
    }
}
