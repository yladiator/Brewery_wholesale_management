using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;
using Brewery_Wholesale_Management.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;

namespace Brewery_wholesale_managementTests
{
    public class BreweryServiceTests
    {
        private readonly DbContextOptions<BreweryDbContext> _options;
        public BreweryServiceTests()
        {
            _options = new DbContextOptionsBuilder<BreweryDbContext>()
                .UseInMemoryDatabase(databaseName: "BreweryTestDB")
                .Options;

            using (var context = new BreweryDbContext(_options))
            {
                if (!context.Wholesalers.Any())
                {
                    context.Wholesalers.Add(new Wholesaler { Id = 1, Name = "Test Wholesaler" });
                    context.Beers.Add(new Beer { Id = 1, Name = "Test Beer", Price = 10.00m, AlcoholContent= 3.6 , BreweryId=1});
                    context.Beers.Add(new Beer { Id = 2, Name = "Test Beer 2", Price = 12.00m, AlcoholContent = 14, BreweryId = 1 });
                    context.Breweries.Add(new Brewery { Id = 1, Name = "Test Brewery" });
                    context.WholesalerStocks.Add(new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 31 });
                    context.WholesalerStocks.Add(new WholesalerStock { WholesalerId = 1, BeerId = 2, Quantity = 45 });
                    context.SaveChanges();
                }
            }
        }

        [Fact]
        public async Task AddBeer_ShouldAddBeerToDatabase()
        {
            // Arrange
            var brewery = new Brewery { Id = 1, Name = "Test Brewery" };
            var beerRequest = new BeerRequestModel
            {
                Name = "Test Beer",
                AlcoholContent = 5.0,
                Price = 2.99m,
                BreweryId = 1
            };

            var service = new BreweryService(new BreweryDbContext(_options));


            // Act
            var result = await service.AddBeer(beerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(beerRequest.Name, result.Name);
            Assert.Equal(beerRequest.AlcoholContent, result.AlcoholContent);
            Assert.Equal(beerRequest.Price, result.Price);
            Assert.Equal(beerRequest.BreweryId, result.BreweryId);

            // check that the beer was added to the beerDbSetMock and saved to the context

            using (var context = new BreweryDbContext(_options))
            {
                var addedBeer = context.Beers.FirstOrDefault(b => b.Id == result.Id);
                Assert.NotNull(addedBeer);
                Assert.Equal(result.Name, addedBeer?.Name);
                Assert.Equal(result.AlcoholContent, addedBeer?.AlcoholContent);
                Assert.Equal(result.Price, addedBeer?.Price);
                Assert.Equal(result.BreweryId, addedBeer?.BreweryId);
            }
        }

        [Fact]
        public async Task AddBeer_WithInvalidBrewery_ReturnsNull()
        {
            // Arrange
            var invalidBreweryId = 999;
            var beerRequest = new BeerRequestModel
            {
                Name = "Test Beer",
                AlcoholContent = 5.0,
                Price = 2.99m,
                BreweryId = invalidBreweryId
            };

            var service = new BreweryService(new BreweryDbContext(_options));

            // Act
            var result = await service.AddBeer(beerRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteBeer_WithValidBeerId_DeletesBeerAndReturnsTrue()
        {
            // Arrange
            var beerId = 1;
            var service = new BreweryService(new BreweryDbContext(_options));

            // Act
            var result = await service.DeleteBeer(beerId);

            // Assert
            Assert.True(result);

            using (var context = new BreweryDbContext(_options))
            {
                var deletedBeer = await context.Beers.FindAsync(beerId);
                Assert.Null(deletedBeer);
            }
        }

        [Fact]
        public async Task DeleteBeer_WithInvalidBeerId_ReturnsFalse()
        {
            // Arrange
            int nonExistentBeerId = 100;
            var service = new BreweryService(new BreweryDbContext(_options));

            // Act
            bool result = await service.DeleteBeer(nonExistentBeerId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetBeersByBrewery_WithValidBreweryId_ReturnsListOfBeers()
        {
            // Arrange
            using var context = new BreweryDbContext(_options);
            var service = new BreweryService(context);
            var breweryId = 1;

            // Act
            var result = await service.GetBeersByBrewery(breweryId);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
     item =>
     {
         Assert.Equal("Test Beer", item.Name);
         Assert.Equal(10.00m, item.Price);
     },
     item =>
     {
         Assert.Equal("Test Beer 2", item.Name);
         Assert.Equal(12.00m, item.Price);
     }
 );

        }

        [Fact]
        public async Task Add_UpdateWholesalerStock_WithValidData_AddsSaleAndReturnsWholesalerStock()
        {
            // Arrange
            using (var context = new BreweryDbContext(_options))
            {
                var service = new BreweryService(context);

                // Act
                var result = await service.Add_UpdateWholesalerStock(1, 1, 61);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.BeerId);
                Assert.Equal(1, result.WholesalerId);
                Assert.Equal(61, result.Quantity);

                var beer = context.Beers.Include(b => b.WholesalerStocks).FirstOrDefault(b => b.Id == 1);
                Assert.NotNull(beer);
                Assert.Equal(61, beer?.WholesalerStocks?.FirstOrDefault(ws => ws.WholesalerId == 1)?.Quantity);
            }
        }
    }
}