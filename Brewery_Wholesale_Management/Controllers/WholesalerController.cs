using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;
using Brewery_Wholesale_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brewery_Wholesale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerController : ControllerBase
    {
        private readonly IWholesalerService _wholesalerService;

        public WholesalerController(IWholesalerService wholesalerService)
        {
            _wholesalerService = wholesalerService;
        }

        [HttpPut("{wholesalerId}/beers/{beerId}/stock/{quantity}")]
        public async Task<IActionResult> UpdateBeerStock(int wholesalerId, int beerId,int quantity)
        {
            try
            {
                bool updated = await _wholesalerService.UpdateBeerStock(wholesalerId, beerId, quantity);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
