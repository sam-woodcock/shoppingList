using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkCoreMock;
using NoMuda.Controllers;
using NoMuda.Models;
using Microsoft.EntityFrameworkCore;

namespace NoMuda.Tests
{


    [TestClass]
    public class ShoppingListControllerTests
    {
        private ShoppingListController _controller;


        [TestInitialize]
        public void TestSetup()
        {


            // Set up options for in-memory database
            var options = new DbContextOptionsBuilder<ShoppingListContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            // Create the actual in-memory database context
            var dbContext = new ShoppingListContext(options);

            // Set up mock DbSet properties
            dbContext.ToBuyList.AddRange(new List<ToBuyListItem>
            {
                new ToBuyListItem { Id = 1, Name = "Item 1", IsImportant = false },
                new ToBuyListItem { Id = 2, Name = "Item 2", IsImportant = true }
            }.AsQueryable());

            dbContext.PreviouslyBoughtList.AddRange(new List<PreviouslyBoughtListItem>
            {
                new PreviouslyBoughtListItem { Id = 3, Name = "Bought Item 1", IsImportant = false },
                new PreviouslyBoughtListItem { Id = 4, Name = "Bought Item 2", IsImportant = true }
            }.AsQueryable());

            dbContext.SaveChanges();


            // Use the Object property to get the instance of IShoppingListContext
            _controller = new ShoppingListController(dbContext);
        }

        [TestMethod]
        public void GetShoppingList_ShouldReturnAllItems()
        {
            // Act
            var result = _controller.GetShoppingList() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);

            var data = result.Value as ShoppingListController.ShoppingListResponse;

            // Access the properties directly from the custom class
            var toBuyList = data.ToBuyList;
            var previouslyBoughtList = data.PreviouslyBoughtList;

            Assert.AreEqual(2, toBuyList.Count());
            Assert.AreEqual(2, previouslyBoughtList.Count());

            // Add other test methods following the same pattern
        }
    }
}
