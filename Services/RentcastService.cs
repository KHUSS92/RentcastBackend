using RestSharp;
using RentcastBackend.Interfaces;
using System.Text.Json;
using RentcastBackend.Models.Property;

public class RentcastService : IRentcastService
{
    private readonly RestClient _client;
    private readonly string _apiKey;

    public RentcastService(IConfiguration configuration)
    {
        // Initialize the RestClient with RentCast's base URL
        _client = new RestClient("https://api.rentcast.io");

        // Retrieve the API key from configuration
        _apiKey = configuration["RentcastApi:ApiKey"];

        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new ArgumentNullException(nameof(_apiKey), "API Key cannot be null or empty. Check your configuration.");
        }

    }

    public async Task<PropertyDetails?> GetProperty(string propertyId)
    {
        if (string.IsNullOrEmpty(propertyId))
        {
            throw new ArgumentNullException(nameof(propertyId), "Property ID cannot be null or empty.");
        }

        var request = new RestRequest($"/v1/properties/{Uri.EscapeDataString(propertyId)}", Method.Get);
        request.AddHeader("X-API-Key", _apiKey);

        var response = await _client.ExecuteAsync(request);

        ValidateResponse(response);

        return JsonSerializer.Deserialize<PropertyDetails>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
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
