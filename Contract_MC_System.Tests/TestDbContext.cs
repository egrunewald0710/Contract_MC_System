using Contract_MC_System;
using Microsoft.EntityFrameworkCore;

namespace Contract_MC_System.Tests
{
    public class TestDbContext : AppDbContext
    {
        private readonly string _dbName;

        public TestDbContext(string dbName)
        {
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_dbName);
        }
    }
}
