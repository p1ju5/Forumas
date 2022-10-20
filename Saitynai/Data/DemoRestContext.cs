using Microsoft.EntityFrameworkCore;
using Saitynai.Data.Entities;

namespace Saitynai.Data
{
    public class DemoRestContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // !!! DON'T STORE THE REAL CONNECTION STRING THE IN PUBLIC REPO !!!
            // Use secret managers provided by your chosen cloud provide
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=RestDemo");
        }
    }
}
