using EEKDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEKDotNetCore.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=.;Initial Catalog=EEKDotNetCore;User ID=sa;Password=sa@123;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            };
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }

}
