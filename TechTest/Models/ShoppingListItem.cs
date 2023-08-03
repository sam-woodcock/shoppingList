using System.ComponentModel.DataAnnotations.Schema;

namespace TechTest.Models
{
    public abstract class ShoppingListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsImportant { get; set; }

        // Parameterless constructor
        public ShoppingListItem()
        {
        }
    }

    [Table("ToBuyList")]
    public class ToBuyListItem : ShoppingListItem
    {
    }

    [Table("PreviouslyBoughtList")]
    public class PreviouslyBoughtListItem : ShoppingListItem
    {
    }
}