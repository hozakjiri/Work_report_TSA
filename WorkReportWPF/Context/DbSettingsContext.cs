using Microsoft.EntityFrameworkCore;
using WorkReportWPF.Models;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF
{
    public class DbSettingsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ./Database/Settings.db", default);
        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
