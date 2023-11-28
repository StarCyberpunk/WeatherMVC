using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WeatherMVC.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<WeatherMVC.Models.Weather> Weathers { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opBild)
        {
            opBild.UseSqlServer("Data Source=RootEmil;Initial Catalog=ds_test;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}