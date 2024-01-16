using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repository
{
    public class BaseDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=Queue.db;");
            }

        }

    }
}
