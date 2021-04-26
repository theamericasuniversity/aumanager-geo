using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aumanager_geo_core.Models;

namespace aumanager_geo_core.Interfaces
{
    public interface ICountryRepository
    {
        public Task<Country> GetCountriesRepository(string? _statesParam);

        public Task<Country> GetCountryRepository(int _id, string? _statesParam);

        public Task<short> PutCountryRepository(int _id, Country _country);

        public Task<bool> PostCountryRepository(Country _country);

        public Task<short> DeleteCountryRepository(int _id);
    }
}
