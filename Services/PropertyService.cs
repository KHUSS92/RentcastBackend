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
            // Save the main property node
            await SavePropertyNode(propertyDetails);

            // Save related nodes and relationships
            await SaveHOA(propertyDetails);
            await SaveFeatures(propertyDetails);
            await SaveTaxAssessments(propertyDetails);
            await SavePropertyTaxes(propertyDetails);
            await SaveHistory(propertyDetails);
            await SaveOwner(propertyDetails);
        }

        private async Task SavePropertyNode(PropertyDetails propertyDetails)
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
        }

        private async Task SaveHOA(PropertyDetails propertyDetails)
        {
            if (propertyDetails.HOA != null)
            {
                var hoaNode = new Dictionary<string, object>
        {
            { "id", $"{propertyDetails.Property.Id}_HOA" },
            { "fee", propertyDetails.HOA.Fee }
        };
                await _neo4jService.CreateNodeAsync("HOA", hoaNode);
                await _neo4jService.CreateRelationshipAsync(propertyDetails.Property.Id, "HAS_HOA", $"{propertyDetails.Property.Id}_HOA");
            }
        }

        private async Task SaveFeatures(PropertyDetails propertyDetails)
        {
            if (propertyDetails.Features != null)
            {
                var featuresNode = new Dictionary<string, object>
        {
            { "id", $"{propertyDetails.Property.Id}_Features" },
            { "architectureType", propertyDetails.Features.ArchitectureType },
            { "garage", propertyDetails.Features.Garage },
            { "pool", propertyDetails.Features.Pool },
            { "heating", propertyDetails.Features.Heating },
            { "cooling", propertyDetails.Features.Cooling }
        };
                await _neo4jService.CreateNodeAsync("Features", featuresNode);
                await _neo4jService.CreateRelationshipAsync(propertyDetails.Property.Id, "HAS_FEATURES", $"{propertyDetails.Property.Id}_Features");
            }
        }

        private async Task SaveTaxAssessments(PropertyDetails propertyDetails)
        {
            if (propertyDetails.TaxAssessments != null)
            {
                foreach (var taxAssessment in propertyDetails.TaxAssessments)
                {
                    var taxNode = new Dictionary<string, object>
            {
                { "id", $"{propertyDetails.Property.Id}_Tax_{taxAssessment.Key}" },
                { "year", taxAssessment.Value.Year },
                { "value", taxAssessment.Value.Value },
                { "land", taxAssessment.Value.Land },
                { "improvements", taxAssessment.Value.Improvements }
            };
                    await _neo4jService.CreateNodeAsync("TaxAssessment", taxNode);
                    await _neo4jService.CreateRelationshipAsync(propertyDetails.Property.Id, "HAS_TAX", $"{propertyDetails.Property.Id}_Tax_{taxAssessment.Key}");
                }
            }
        }

        private async Task SavePropertyTaxes(PropertyDetails propertyDetails)
        {
            if (propertyDetails.PropertyTaxes != null)
            {
                foreach (var propertyTax in propertyDetails.PropertyTaxes)
                {
                    var taxNode = new Dictionary<string, object>
            {
                { "id", $"{propertyDetails.Property.Id}_PropertyTax_{propertyTax.Key}" },
                { "year", propertyTax.Value.Year },
                { "total", propertyTax.Value.Total }
            };
                    await _neo4jService.CreateNodeAsync("PropertyTax", taxNode);
                    await _neo4jService.CreateRelationshipAsync(propertyDetails.Property.Id, "HAS_PROPERTY_TAX", $"{propertyDetails.Property.Id}_PropertyTax_{propertyTax.Key}");
                }
            }
        }

        private async Task SaveHistory(PropertyDetails propertyDetails)
        {
            if (propertyDetails.History != null)
            {
                foreach (var historyEvent in propertyDetails.History)
                {
                    var historyNode = new Dictionary<string, object>
            {
                { "id", $"{propertyDetails.Property.Id}_History_{historyEvent.Key}" },
                { "event", historyEvent.Value.Event },
                { "date", historyEvent.Value.Date },
                { "price", historyEvent.Value.Price }
            };
                    await _neo4jService.CreateNodeAsync("History", historyNode);
                    await _neo4jService.CreateRelationshipAsync(propertyDetails.Property.Id, "HAS_HISTORY", $"{propertyDetails.Property.Id}_History_{historyEvent.Key}");
                }
            }
        }

        private async Task SaveOwner(PropertyDetails propertyDetails)
        {
            if (propertyDetails.Owner != null)
            {
                var ownerNode = new Dictionary<string, object>
        {
            { "id", $"{propertyDetails.Property.Id}_Owner" },
            { "formattedAddress", propertyDetails.Owner.FormattedAddress },
            { "addressLine1", propertyDetails.Owner.AddressLine1 },
            { "addressLine2", propertyDetails.Owner.AddressLine2 },
            { "city", propertyDetails.Owner.City },
            { "state", propertyDetails.Owner.State },
            { "zipCode", propertyDetails.Owner.ZipCode }
        };
                await _neo4jService.CreateNodeAsync("Owner", ownerNode);
                await _neo4jService.CreateRelationshipAsync(propertyDetails.Property.Id, "OWNED_BY", $"{propertyDetails.Property.Id}_Owner");
            }
        }
    }
}
