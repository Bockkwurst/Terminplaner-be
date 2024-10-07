using Microsoft.EntityFrameworkCore;
using Terminplaner_be.Models;

namespace Terminplaner_be.Context
{
    public class TerminplanerDbContext : DbContext
    {
        private readonly string dbPath;
        public DbSet<AppointmentEntity> Appointments { get ; set; }
        public DbSet<UserEntity> Users { get; set; }

        public TerminplanerDbContext() 
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            dbPath = Path.Combine(path, "Terminplaner.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
