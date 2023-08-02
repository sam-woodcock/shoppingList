// ShoppingListController.cs
using Microsoft.AspNetCore.Mvc;
using TechTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace TechTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : ControllerBase
    {
        private readonly ShoppingListContext _context;

        public ShoppingListController(ShoppingListContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetShoppingList()
        {
            var shoppingList = _context.ShoppingList.ToList();
            return Ok(shoppingList);
        }

        [HttpPost]
        public IActionResult AddShoppingListItem(ShoppingListItem item)
        {
            _context.ShoppingList.Add(item);
            _context.SaveChanges(); // Save changes to the database
            return CreatedAtAction(nameof(GetShoppingList), new { id = item.Id }, item);
        }


[HttpPut("{id}/markAsBought")]
        public IActionResult MarkAsBought(int id)
        {
            var item = _context.ShoppingList.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsBought = true;
            _context.SaveChanges(); // Save changes to the database
            return NoContent();
        }

        [HttpPut("{id}/markAsNotBought")]
        public IActionResult MarkAsNotBought(int id)
        {
            var item = _context.ShoppingList.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsBought = false;
            _context.SaveChanges(); // Save changes to the database
            return NoContent();
        }

        [HttpPut("{id}/moveToPreviouslyBought")]
        public IActionResult MoveToPreviouslyBought(int id)
        {
            var item = _context.ShoppingList.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            // Move the item to the "Previously Bought" list
            item.IsBought = true;
            _context.SaveChanges(); // Save changes to the database
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShoppingListItem(int id, ShoppingListItem updatedItem)
        {
            var existingItem = _context.ShoppingList.FirstOrDefault(item => item.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            // Update the properties of the existing item with the properties of the updated item
            existingItem.Name = updatedItem.Name;
            existingItem.IsBought = updatedItem.IsBought;
            existingItem.IsImportant = updatedItem.IsImportant;
            _context.SaveChanges(); // Save changes to the database
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingListItem(int id)
        {
            var itemToRemove = _context.ShoppingList.FirstOrDefault(item => item.Id == id);
            if (itemToRemove == null)
            {
                return NotFound();
            }

            _context.ShoppingList.Remove(itemToRemove);
            _context.SaveChanges(); // Save changes to the database
            return NoContent();
        }
    }
}
