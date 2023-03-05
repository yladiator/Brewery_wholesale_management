using Brewery_Wholesale_Management.Model;
using Brewery_Wholesale_Management.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewery_wholesale_managementTests
{
    public class WholesalerServiceTests
    {
        private readonly DbContextOptions<BreweryDbContext> _options;

        public WholesalerServiceTests()
        {
            // Use an in-memory database for testing
            _options = new DbContextOptionsBuilder<BreweryDbContext>()
                .UseInMemoryDatabase(databaseName: "WholesalerTestDB")
                .Options;

            //Create a new in-memory database context
            using (var context = new BreweryDbContext(_options))
            {
                // Seed the database with test data
                context.Database.EnsureCreated();
                if (!context.Wholesalers.Any())
                {
                    context.Wholesalers.Add(new Wholesaler { Id = 1, Name = "Test Wholesaler" });
                    context.Beers.Add(new Beer { Id = 1, Name = "Test Beer", Price = 10.00m });
                    context.Beers.Add(new Beer { Id = 2, Name = "Test Beer 2", Price = 12.00m });
                    context.Breweries.Add(new Brewery { Id = 1, Name = "Test Brewery 2" });
                    context.WholesalerStocks.Add(new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 31 });
                    context.WholesalerStocks.Add(new WholesalerStock { WholesalerId = 1, BeerId = 2, Quantity = 45 });
                    context.SaveChanges();
                }
            }

        }

        [Fact]
        public void UpdateBeerStock_WithValidData_ShouldUpdateWholesalerStock()
        {
            // Arrange
            var wholesalerId = 1;
            var beerId = 1;
            var newQuantity = 30;



            // Act
            using (var context = new BreweryDbContext(_options))
            {
                var service = new WholesalerService(context);
                var result = service.UpdateBeerStock(wholesalerId, beerId, newQuantity);

                // Assert
                Assert.True(result);

                var updatedStock = context.WholesalerStocks.FirstOrDefault(ws => ws.WholesalerId == wholesalerId && ws.BeerId == beerId);
                Assert.NotNull(updatedStock);
                Assert.Equal(newQuantity, updatedStock?.Quantity);
            }
        }

        [Fact]
        public void UpdateBeerStock_WithInvalidWholesalerId_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidWholesalerId = 2;
            var beerId = 1;
            var quantity = 20;

            // Act
            using (var context = new BreweryDbContext(_options))
            {
                var service = new WholesalerService(context);

                // Assert

                var ex = Assert.Throws<ArgumentException>(() => service.UpdateBeerStock(invalidWholesalerId, beerId, quantity));
                Assert.Equal($"Wholesaler with ID {invalidWholesalerId} does not have beer with ID {beerId} in their inventory.", ex.Message);
            }
        }

        [Fact]
        public void UpdateBeerStock_WithInvalidBeerId_ShouldThrowArgumentException()
        {
            // Arrange
            var wholesalerId = 1;
            var invalidBeerId = 3;
            var quantity = 20;
            // Act
            using (var context = new BreweryDbContext(_options))
            {

                var service = new WholesalerService(context);
               
                // Act and Assert
                var ex = Assert.Throws<ArgumentException>(() => service.UpdateBeerStock(wholesalerId, invalidBeerId, quantity));
                Assert.Equal($"Wholesaler with ID {wholesalerId} does not have beer with ID {invalidBeerId} in their inventory.", ex.Message);
            }
        }
    }
}
