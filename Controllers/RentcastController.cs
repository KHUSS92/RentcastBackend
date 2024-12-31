using Microsoft.AspNetCore.Mvc;
using RentcastBackend.Interfaces;

namespace RentcastBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentcastController : ControllerBase
    {
        private readonly IRentcastService _rentcastService;

        public RentcastController(IRentcastService rentcastService)
        {
            _rentcastService = rentcastService;
        }

        [HttpGet("propertyID/{id}")]
        public async Task<ActionResult<String>> GetPropertyByID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Property ID cannot be null or empty.");
            }

            // Decode the propertyId to avoid double encoding
            var decodedId = Uri.UnescapeDataString(id);

            try
            {
                // Log the decoded ID to verify it's correct
                Console.WriteLine($"Decoded Property ID: {decodedId}");

                // Call the RentcastService to fetch property data
                var propertyData = await _rentcastService.GetProperty(id);
                return Ok(propertyData);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
