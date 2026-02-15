using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElderCareApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ElderCareApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=ElderCareDb;user=root;password=1234; SslMode=none;");
        }
        public DbSet<CareHome> CareHomes { get; set; } 
        public DbSet<Elder> Elders { get; set; }  

        public DbSet<Staff> Staffs { get; set; }
public DbSet<ElderHealthRecord> ElderHealthRecords { get; set; }
    }

    
}