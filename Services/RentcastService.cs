using RestSharp;
using RentcastBackend.Interfaces;
using RentcastBackend.Models.Property;
using Newtonsoft.Json;

public class RentcastService : IRentcastService
{
    private readonly RestClient _client;
    private readonly string _apiKey;
    private readonly ILogger<RentcastService> _logger;

    public RentcastService(IConfiguration configuration, ILogger<RentcastService> logger)
    {
        // Initialize the RestClient with RentCast's base URL
        _client = new RestClient("https://api.rentcast.io");

        // Retrieve the API key from configuration
        _apiKey = configuration["RentcastApi:ApiKey"];

        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new ArgumentNullException(nameof(_apiKey), "API Key cannot be null or empty. Check your configuration.");
        }

        _logger = logger;
    }

    public async Task<PropertyDetails?> GetProperty(string propertyId)
    {
        if (string.IsNullOrEmpty(propertyId))
        {
            throw new ArgumentNullException(nameof(propertyId), "Property ID cannot be null or empty.");
        }

        var encodedPropertyId = Uri.IsWellFormedUriString(propertyId, UriKind.RelativeOrAbsolute) ? propertyId : Uri.EscapeDataString(propertyId);

        var request = new RestRequest($"/v1/properties/{Uri.EscapeDataString(propertyId)}", Method.Get);
        request.AddHeader("X-API-Key", _apiKey);

        var response = await _client.ExecuteAsync(request);

        // Log the raw response for debugging purposes
        if (!response.IsSuccessful)
        {
            _logger.LogError("Failed to fetch property with ID {PropertyId}. Status Code: {StatusCode}, Content: {Content}",
                             propertyId, response.StatusCode, response.Content);
            throw new HttpRequestException($"Failed to fetch property with ID {propertyId}. Status Code: {response.StatusCode}");
        }

        // Log the successful raw JSON response
        _logger.LogInformation("Successfully fetched property with ID {PropertyId}. Response: {ResponseContent}",
                               propertyId, response.Content);

        ValidateResponse(response);

        return JsonConvert.DeserializeObject<PropertyDetails>(response.Content);
    }

    //Maintain Seperation of Concerns
    private void ValidateResponse(RestResponse response)
    {

        if (response is null)
            throw new ApplicationException("No response from server.");

        if (!response.IsSuccessful)
            throw new ApplicationException($"Error fetching property details: {response.ErrorMessage ?? "Unknown error."}");

        if (string.IsNullOrWhiteSpace(response.Content))
            throw new ApplicationException("The API returned an empty response.");
    }
}
