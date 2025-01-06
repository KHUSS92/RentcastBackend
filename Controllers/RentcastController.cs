using Microsoft.AspNetCore.Mvc;
using RentcastBackend.Interfaces;
using RentcastBackend.Models.Property;

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
        public async Task<ActionResult<PropertyDetails>> GetPropertyByID(string id)
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
                var propertyDetails = await _rentcastService.GetProperty(id);

                return Ok(propertyDetails);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception details
                Console.WriteLine($"Error fetching property data for ID: {decodedId}. Exception: {ex.Message}");
                return StatusCode(500, $"Error fetching property data: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}
