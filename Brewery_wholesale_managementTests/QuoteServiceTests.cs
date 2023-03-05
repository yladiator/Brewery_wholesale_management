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
    public class QuoteServiceTests
    {
        private readonly DbContextOptions<BreweryDbContext> _options;

        public QuoteServiceTests()
        {
            _options = new DbContextOptionsBuilder<BreweryDbContext>()
                .UseInMemoryDatabase(databaseName: "QuotesTestDB")
                .Options;

            using (var context = new BreweryDbContext(_options))
            {
                if (!context.Wholesalers.Any())
                {
                    context.Wholesalers.Add(new Wholesaler { Id = 1, Name = "Test Wholesaler" });
                    context.Beers.Add(new Beer { Id = 1, Name = "Test Beer", Price = 10.00m });
                    context.Beers.Add(new Beer { Id = 2, Name = "Test Beer 2", Price = 12.00m });
                    context.Breweries.Add(new Brewery { Id = 1, Name = "Test Brewery" });
                    context.WholesalerStocks.Add(new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 31 });
                    context.WholesalerStocks.Add(new WholesalerStock { WholesalerId = 1, BeerId = 2, Quantity = 45 });
                    context.SaveChanges();
                }
            }
        }

        [Fact]
        public void RequestQuote_WithEmptyQuoteRequestItems_ShouldThrowArgumentException()
        {
            // Arrange
            var service = new QuoteService(new BreweryDbContext(_options));

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.RequestQuote(1, new List<QuoteRequestItem>()));
        }

        [Fact]
        public void RequestQuote_WithNonExistingWholesalerId_ShouldThrowArgumentException()
        {
            // Arrange
            var service = new QuoteService(new BreweryDbContext(_options));
            var quoteRequestItems = new List<QuoteRequestItem> { new QuoteRequestItem { BeerId = 1, Quantity = 5 } };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.RequestQuote(2, quoteRequestItems));
        }

        [Fact]
        public void RequestQuote_WithDuplicateBeerIds_ShouldThrowArgumentException()
        {
            // Arrange
            var service = new QuoteService(new BreweryDbContext(_options));
            var quoteRequestItems = new List<QuoteRequestItem>
            {
                new QuoteRequestItem { BeerId = 1, Quantity = 5 },
                new QuoteRequestItem { BeerId = 1, Quantity = 3 }
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.RequestQuote(1, quoteRequestItems));
        }

        [Fact]
        public void RequestQuote_WithNonExistingBeerId_ShouldThrowArgumentException()
        {
            // Arrange
            var service = new QuoteService(new BreweryDbContext(_options));
            var quoteRequestItems = new List<QuoteRequestItem> { new QuoteRequestItem { BeerId = 3, Quantity = 5 } };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.RequestQuote(1, quoteRequestItems));
        }

        [Fact]
        public void RequestQuote_WithNonSoldBeerId_ShouldThrowArgumentException()
        {
            // Arrange
            var service = new QuoteService(new BreweryDbContext(_options));
            var quoteRequestItems = new List<QuoteRequestItem> { new QuoteRequestItem { BeerId = 1, Quantity = 5 } };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.RequestQuote(2, quoteRequestItems));
        }

        [Fact]
        public void RequestQuote_WithInsufficientBeerStock()
        {
            // Arrange
            var service = new QuoteService(new BreweryDbContext(_options));
            var quoteRequestItems = new List<QuoteRequestItem> { new QuoteRequestItem { BeerId = 1, Quantity = 50 } };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.RequestQuote(1, quoteRequestItems));
        }

        [Fact]
        public void RequestQuote_WithValidData_ShouldReturnValidQuoteResponse_lessthan10_return170()
        {
            // Arrange
            var quoteRequestItems = new List<QuoteRequestItem>
    {
        new QuoteRequestItem { BeerId = 1, Quantity = 5 },
        new QuoteRequestItem { BeerId = 2, Quantity = 10 }
    };
            var wholesaler = new Wholesaler { Id = 1, Name = "Test Wholesaler" };
            var beer1 = new Beer { Id = 1, Name = "Test Beer 1", Price = 10.00m };
            var beer2 = new Beer { Id = 2, Name = "Test Beer 2", Price = 12.00m };
            var wholesalerStock1 = new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 20 };
            var wholesalerStock2 = new WholesalerStock { WholesalerId = 1, BeerId = 2, Quantity = 15 };


            var service = new QuoteService(new BreweryDbContext(_options));

            // Act
            var result = service.RequestQuote(1, quoteRequestItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(170.00m, result.TotalPrice);
            Assert.NotNull(result.Wholesaler);
            Assert.Equal("Test Wholesaler", result.Wholesaler.Name);
            Assert.NotNull(result.Summary);
            Assert.Equal(2, result.Summary.Count);
            Assert.Equal(5, result.Summary.First(s => s.BeerId == 1).Quantity);
            Assert.Equal(10, result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(50.00m, result.Summary.First(s => s.BeerId == 1).Quantity * result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(10, result.Summary.First(s => s.BeerId == 2).Quantity);
            Assert.Equal(12, result.Summary.First(s => s.BeerId == 2).Price);
            Assert.Equal(120.00m, result.Summary.First(s => s.BeerId == 2).Quantity * result.Summary.First(s => s.BeerId == 2).Price);
        }

        [Fact]
        public void RequestQuote_WithValidData_ShouldReturnValidQuoteResponse_biggerthan10_return_217_8()
        {
            // Arrange
            var quoteRequestItems = new List<QuoteRequestItem>
    {
        new QuoteRequestItem { BeerId = 1, Quantity = 11 },
        new QuoteRequestItem { BeerId = 2, Quantity = 16 }
    };
            var wholesaler = new Wholesaler { Id = 1, Name = "Test Wholesaler" };
            var beer1 = new Beer { Id = 1, Name = "Test Beer 1", Price = 10.00m };
            var beer2 = new Beer { Id = 2, Name = "Test Beer 2", Price = 12.00m };
            var wholesalerStock1 = new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 20 };
            var wholesalerStock2 = new WholesalerStock { WholesalerId = 1, BeerId = 2, Quantity = 15 };


            var service = new QuoteService(new BreweryDbContext(_options));

            // Act
            var result = service.RequestQuote(1, quoteRequestItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(271.80m, result.TotalPrice);
            Assert.NotNull(result.Wholesaler);
            Assert.Equal("Test Wholesaler", result.Wholesaler.Name);
            Assert.NotNull(result.Summary);
            Assert.Equal(2, result.Summary.Count);
            Assert.Equal(11, result.Summary.First(s => s.BeerId == 1).Quantity);
            Assert.Equal(9, result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(99, result.Summary.First(s => s.BeerId == 1).Quantity * result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(16, result.Summary.First(s => s.BeerId == 2).Quantity);
            Assert.Equal(10.80m, result.Summary.First(s => s.BeerId == 2).Price);
            Assert.Equal(172.80m, result.Summary.First(s => s.BeerId == 2).Quantity * result.Summary.First(s => s.BeerId == 2).Price);
        }

        [Fact]
        public void RequestQuote_WithValidData_ShouldReturnValidQuoteResponse_biggerthan20_return_388_8()
        {
            // Arrange
            var quoteRequestItems = new List<QuoteRequestItem>
    {
        new QuoteRequestItem { BeerId = 1, Quantity = 21 },
        new QuoteRequestItem { BeerId = 2, Quantity = 23 }
    };
            var wholesaler = new Wholesaler { Id = 1, Name = "Test Wholesaler" };
            var beer1 = new Beer { Id = 1, Name = "Test Beer 1", Price = 10.00m };
            var beer2 = new Beer { Id = 2, Name = "Test Beer 2", Price = 12.00m };
            var wholesalerStock1 = new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 20 };
            var wholesalerStock2 = new WholesalerStock { WholesalerId = 1, BeerId = 2, Quantity = 15 };


            var service = new QuoteService(new BreweryDbContext(_options));

            // Act
            var result = service.RequestQuote(1, quoteRequestItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(388.80m, result.TotalPrice);
            Assert.NotNull(result.Wholesaler);
            Assert.Equal("Test Wholesaler", result.Wholesaler.Name);
            Assert.NotNull(result.Summary);
            Assert.Equal(2, result.Summary.Count);
            Assert.Equal(21, result.Summary.First(s => s.BeerId == 1).Quantity);
            Assert.Equal(8, result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(168, result.Summary.First(s => s.BeerId == 1).Quantity * result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(23, result.Summary.First(s => s.BeerId == 2).Quantity);
            Assert.Equal(9.60m, result.Summary.First(s => s.BeerId == 2).Price);
            Assert.Equal(220.80m, result.Summary.First(s => s.BeerId == 2).Quantity * result.Summary.First(s => s.BeerId == 2).Price);
        }


        [Fact]
        public void RequestQuote_WithValidData_ShouldReturnValidQuoteResponse_Beer1biggerthan10_beer2biggerthan20_return_319_8()
        {
            // Arrange
            var quoteRequestItems = new List<QuoteRequestItem>
    {
        new QuoteRequestItem { BeerId = 1, Quantity = 11 },
        new QuoteRequestItem { BeerId = 2, Quantity = 23 }
    };
            var wholesaler = new Wholesaler { Id = 1, Name = "Test Wholesaler" };
            var beer1 = new Beer { Id = 1, Name = "Test Beer 1", Price = 10.00m };
            var beer2 = new Beer { Id = 2, Name = "Test Beer 2", Price = 12.00m };
            var wholesalerStock1 = new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 20 };
            var wholesalerStock2 = new WholesalerStock { WholesalerId = 1, BeerId = 2, Quantity = 15 };


            var service = new QuoteService(new BreweryDbContext(_options));

            // Act
            var result = service.RequestQuote(1, quoteRequestItems);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(319.80m, result.TotalPrice);
            Assert.NotNull(result.Wholesaler);
            Assert.Equal("Test Wholesaler", result.Wholesaler.Name);
            Assert.NotNull(result.Summary);
            Assert.Equal(2, result.Summary.Count);
            Assert.Equal(11, result.Summary.First(s => s.BeerId == 1).Quantity);
            Assert.Equal(9, result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(99, result.Summary.First(s => s.BeerId == 1).Quantity * result.Summary.First(s => s.BeerId == 1).Price);
            Assert.Equal(23, result.Summary.First(s => s.BeerId == 2).Quantity);
            Assert.Equal(9.60m, result.Summary.First(s => s.BeerId == 2).Price);
            Assert.Equal(220.80m, result.Summary.First(s => s.BeerId == 2).Quantity * result.Summary.First(s => s.BeerId == 2).Price);
        }
    }
}
