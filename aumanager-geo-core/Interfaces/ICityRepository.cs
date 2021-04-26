using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aumanager_geo_core.Models;

namespace aumanager_geo_core.Interfaces
{
    public interface ICityRepository
    {
        public Task<City> GetCitiesRepository();

        public Task<State> GetCityRepository(int _id);

        public Task<short> PutCityRepository(int _id, City _cities);

        public Task<bool> PostCityRepository(City _cities);

        public Task<short> DeleteCityRepository(int _id);
    }
}
