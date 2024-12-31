using RentcastBackend.Models.Property;

namespace RentcastBackend.Services
{
    public class PropertyService
    {
        private readonly RentcastService _rentcastService;
        private readonly Neo4jService _neo4jService;

        public PropertyService(RentcastService rentcastService, Neo4jService neo4jService)
        {
            _rentcastService = rentcastService;
            _neo4jService = neo4jService;
        }
        public async Task<PropertyDetails> GetPropertyByIdAsync(string propertyId)
        {
            // Step 1: Fetch property details from Rentcast API
            var propertyDetails = await _rentcastService.GetProperty(propertyId);

            if (propertyDetails == null)
            {
                return null; // Property not found in Rentcast API
            }

            // Step 2: Save or update property details in Neo4j
            await SavePropertyToNeo4jAsync(propertyDetails);

            return propertyDetails;
        }

        private async Task SavePropertyToNeo4jAsync(PropertyDetails propertyDetails)
        {
            var propertyNode = new Dictionary<string, object>
        {
            { "id", propertyDetails.Property.Id },
            { "formattedAddress", propertyDetails.Property.FormattedAddress },
            { "city", propertyDetails.Property.City },
            { "state", propertyDetails.Property.State },
            { "zipCode", propertyDetails.Property.ZipCode },
            { "ownerOccupied", propertyDetails.OwnerOccupied }
        };

            await _neo4jService.CreateNodeAsync("Property", propertyNode);

            // Add other nodes (e.g., HOA, Features) and relationships if needed
            if (propertyDetails.HOA != null)
            {
                // Create HOA node
                var hoaNode = new Dictionary<string, object>
                {
                    { "id", $"{propertyDetails.Property.Id}_HOA" }, // Unique ID for the HOA node
                    { "fee", propertyDetails.HOA.Fee }
                };

                await _neo4jService.CreateNodeAsync("HOA", hoaNode);

                // Create relationship between Property and HOA
                await _neo4jService.CreateRelationshipAsync(
                    propertyDetails.Property.Id,
                    "HAS_HOA",
                    $"{propertyDetails.Property.Id}_HOA"); // Reference HOA node by its ID
                }

            // Repeat for Features, TaxAssessments, PropertyTaxes, and History
        }
    }
}
