using Microsoft.EntityFrameworkCore;
using WorkReportWPF.Models.DBModels;

namespace WorkReportWPF
{
    public class DbDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ./Database/Data.db", default);
        }
        public DbSet<Data> Datas { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}