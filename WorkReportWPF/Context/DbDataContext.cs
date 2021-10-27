using Microsoft.EntityFrameworkCore;
using WorkReportWPF.Models;

namespace WorkReportWPF
{
    public class DbDataContext2 : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ./Database/Data.db", default);
        }

    }
}