using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Company.Models;

namespace Task_Company.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
       public DbSet<Consumption> Consumptions { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4UT1SMN\\SQLEXPRESS;Database=TaskCompanyDB;Trusted_Connection=true;MultipleActiveResultSets=True;");
            }
        }
    }
}
