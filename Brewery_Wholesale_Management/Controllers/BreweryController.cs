using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;
using Brewery_Wholesale_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Brewery_Wholesale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private readonly IBreweryService _breweryService;
        public BreweryController(IBreweryService breweryService)
        {
            _breweryService = breweryService;

        }

        [HttpGet("{breweryId}/beers")]
        public ActionResult<List<Beer>> GetBeers(int breweryId)
        {
            var beers = _breweryService.GetBeersByBrewery(breweryId);
            if (beers == null || !beers.Any())
            {
                return NotFound();
            }
            return Ok(beers);
        }

        [HttpPost("Add/beers")]
        public ActionResult<Beer> AddBeer(BeerRequestModel request)
        {
           

            // Add the new beer object to the brewery service
            var addedBeer = _breweryService.AddBeer(request);

            if (addedBeer == null)
            {
                return BadRequest();
            }

            // Return a CreatedAtAction result with the newly added beer object
            return CreatedAtAction(nameof(GetBeers), new { addedBeer.BreweryId }, addedBeer);
        }

       

        [HttpDelete("{breweryId}/beers/{beerId}")]
        public IActionResult DeleteBeer(int breweryId, int beerId)
        {
            var result = _breweryService.DeleteBeer(beerId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("{beerId}/sale")]
        public IActionResult AddSale(int beerId, int wholesalerId, int quantity)
        {
            var result = _breweryService.AddOrUpdateWholesalerStock(beerId, wholesalerId, quantity);
            if (result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }


}
