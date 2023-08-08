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

        public class ShoppingListResponse
        {
            public IEnumerable<ToBuyListItem>? ToBuyList { get; set; }
            public IEnumerable<PreviouslyBoughtListItem>? PreviouslyBoughtList { get; set; }
        }


        [HttpGet]
        public IActionResult GetShoppingList()
        {
            var toBuyList = _context.ToBuyList.ToList();
            var previouslyBoughtList = _context.PreviouslyBoughtList.ToList();
            var response = new ShoppingListResponse
            {
                ToBuyList = toBuyList,
                PreviouslyBoughtList = previouslyBoughtList
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddShoppingListItem(ToBuyListItem item)
        {
            _context.ToBuyList.Add((ToBuyListItem)item);
            _context.SaveChanges(); // Save changes to the database
            return CreatedAtAction(nameof(GetShoppingList), new { id = item.Id }, item);
        }


        //[HttpPut("{id}/markAsBought")]
        //        public IActionResult MarkAsBought(int id)
        //        {
        //            var item = _context.ShoppingList.FirstOrDefault(i => i.Id == id);
        //            if (item == null)
        //            {
        //                return NotFound();
        //            }

        //            item.IsBought = true;
        //            _context.SaveChanges(); // Save changes to the database
        //            return NoContent();
        //        }

        //        [HttpPut("{id}/markAsNotBought")]
        //        public IActionResult MarkAsNotBought(int id)
        //        {
        //            var item = _context.ShoppingList.FirstOrDefault(i => i.Id == id);
        //            if (item == null)
        //            {
        //                return NotFound();
        //            }

        //            item.IsBought = false;
        //            _context.SaveChanges(); // Save changes to the database
        //            return NoContent();
        //        }

        [HttpPut("{id}/moveToPreviouslyBought")]
        public IActionResult MoveToPreviouslyBought(ToBuyListItem item)
        {

            var newItem = new PreviouslyBoughtListItem
            {
                Name = item.Name,
                IsImportant = false,//task 3 mark as not important
                // Add any other properties you need to copy
            };

            _context.PreviouslyBoughtList.Add(newItem);
            _context.ToBuyList.Remove(item);

            _context.SaveChanges();
            return CreatedAtAction(nameof(GetShoppingList), new { id = item.Id }, item);

        }

        [HttpPut("{id}/moveToBuy")]
        public IActionResult moveToBuy(PreviouslyBoughtListItem item)
        {

            var newItem = new ToBuyListItem
            {
                Name = item.Name,
                IsImportant = item.IsImportant,
                // Add any other properties you need to copy
            };

            _context.ToBuyList.Add(newItem);
            _context.PreviouslyBoughtList.Remove(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetShoppingList), new { id = item.Id }, item);

        }


        [HttpPut("{id}/delete")]
        public IActionResult delete(int id, ToBuyListItem item)
        {

            var existingItem = _context.ToBuyList.FirstOrDefault(item => item.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _context.ToBuyList.Remove(existingItem);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpPut("{id}")]
        //only items in active list e.g to buy can be updated
        public IActionResult UpdateShoppingListItem(int id, ToBuyListItem updatedItem)
        {
            var existingItem = _context.ToBuyList.FirstOrDefault(item => item.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            // Update the properties of the existing item with the properties of the updated item
            existingItem.Name = updatedItem.Name;
            existingItem.IsImportant = updatedItem.IsImportant;
            _context.SaveChanges(); // Save changes to the database
            return NoContent();
        }


    }
}
