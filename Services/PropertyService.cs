using Newtonsoft.Json;
using RentcastBackend.Models.Property;
using RentcastBackend.Services;
using RestSharp;

public class PropertyService : IPropertyService
{
    private readonly RestClient _client;
    private readonly string _apiKey;
    private readonly ILogger<PropertyService> _logger;

    public PropertyService(IConfiguration configuration, ILogger<PropertyService> logger)
    {
        _apiKey = configuration["RentcastApi:ApiKey"];
        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new ArgumentNullException(nameof(_apiKey), "API Key cannot be null or empty. Check your configuration.");
        }

        _client = new RestClient("https://api.rentcast.io");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PropertyDetails?> GetProperty(string propertyId)
    {
        if (string.IsNullOrEmpty(propertyId))
        {
            throw new ArgumentNullException(nameof(propertyId), "Property ID cannot be null or empty.");
        }

        // Build the request to Rentcast API
        var request = new RestRequest($"/v1/properties/{Uri.EscapeDataString(propertyId)}", Method.Get);
        request.AddHeader("X-API-Key", _apiKey);

        // Execute the API call
        var response = await _client.ExecuteAsync(request);

        // Handle unsuccessful response
        if (!response.IsSuccessful)
        {
            _logger.LogError("Failed to fetch property with ID {PropertyId}. Status Code: {StatusCode}, Content: {Content}",
                             propertyId, response.StatusCode, response.Content);
            throw new HttpRequestException($"Failed to fetch property with ID {propertyId}. Status Code: {response.StatusCode}");
        }

        // Ensure response content is not null or empty
        if (string.IsNullOrWhiteSpace(response.Content))
        {
            throw new InvalidOperationException("The response content is null or empty.");
        }

        // Log raw JSON response for debugging
        _logger.LogInformation("Successfully fetched property with ID {PropertyId}. Response: {ResponseContent}",
                               propertyId, response.Content);

        try
        {
            // Deserialize the response content into PropertyDetails
            var propertyDetails = JsonConvert.DeserializeObject<PropertyDetails>(response.Content);

            if (propertyDetails == null)
            {
                _logger.LogWarning("Deserialization resulted in null for PropertyDetails. JSON: {Json}", response.Content);
                throw new InvalidOperationException("Failed to deserialize PropertyDetails.");
            }

            return propertyDetails;
        }
        catch (JsonException ex)
        {
            _logger.LogError("Deserialization error: {Message}. JSON: {Json}", ex.Message, response.Content);
            throw;
        }
    }
}
