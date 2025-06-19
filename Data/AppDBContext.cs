using Microsoft.EntityFrameworkCore;
using ST10390916_PROG7311_POE.Models;

namespace ST10390916_PROG7311_POE.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=PROG7311DBVolschenk;Trusted_Connection=True");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }

}
