// ShoppingListContext.cs
using Microsoft.EntityFrameworkCore;

namespace TechTest.Models
{
    public class ShoppingListContext : DbContext
    {

        public virtual DbSet<ToBuyListItem> ToBuyList { get; set; }
        public virtual DbSet<PreviouslyBoughtListItem> PreviouslyBoughtList { get; set; }

        public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
            : base(options)
        {
        }

    }


}