using Microsoft.EntityFrameworkCore;
using WorkReportWPF.Models;

namespace WorkReportWPF
{
    public class DbSettingsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ./Database/Settings.db", default);
        }

        public DbSet<Login> Logins { get; set; }
    }
}
