using Microsoft.AspNetCore.Mvc;
using RentcastBackend.Services;

namespace RentcastBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _propertyService;

        public PropertyController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(string id)
        {
            var propertyDetails = await _propertyService.GetPropertyByIdAsync(id);

            if (propertyDetails == null)
            {
                return NotFound("Property not found.");
            }

            return Ok(propertyDetails);
        }
    }
}
