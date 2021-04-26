using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aumanager_geo_core.Models;

namespace aumanager_geo_core.Interfaces
{
    public interface IStateRepository
    {
        public Task<State> GetStatesRepository(string? _citiesParam);

        public Task<State> GetStateRepository(int _id, string? _citiesParam);

        public Task<short> PutStateRepository(int _id, State _country);

        public Task<bool> PostStateRepository(State _country);

        public Task<short> DeleteStateRepository(int _id);
    }
}
