using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using aumanager_geo_core.Models;
using Microsoft.Extensions.Configuration;

namespace aumanager_geo_infra.Configuration
{
    public class GeoContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        private IConfiguration _configuration;

        public GeoContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration["ServiceDBConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
