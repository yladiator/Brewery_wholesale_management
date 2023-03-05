using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brewery_Wholesale_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
             
        public QuoteController(IQuoteService quoteService)
        {      
            _quoteService = quoteService;
        }

        [HttpPost("{wholesalerId}/quote")]
        public IActionResult RequestQuote(int wholesalerId, [FromBody] IEnumerable<QuoteRequestItem> quoteRequestItems)
        {
            try
            {
                var quoteResponse = _quoteService.RequestQuote(wholesalerId, quoteRequestItems);
                return Ok(quoteResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
