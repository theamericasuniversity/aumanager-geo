using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using aumanager_geo_core.Models;

namespace aumanager_geo_infra.Configuration
{
    public class GeoContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-UVRP7RH\SQLEXPRESS;Initial Catalog=aumanager-geo-db;Persist Security Info=False;User ID=sa;Password=3wuutxsx@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            optionsBuilder.UseSqlServer(@"Server=tcp:aumanager-sqlsvr.database.windows.net,1433;Initial Catalog=aumanager-geo-db;Persist Security Info=False;User ID=auadmin;Password=Admin@123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
