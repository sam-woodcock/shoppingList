// ShoppingListContext.cs
using Microsoft.EntityFrameworkCore;

namespace TechTest.Models
{
    public class ShoppingListContext : DbContext
    {
        public DbSet<ShoppingListItem> ShoppingList { get; set; }

        public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
            : base(options) { }
        


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}