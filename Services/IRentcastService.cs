using RentcastBackend.Models.Property;

namespace RentcastBackend.Interfaces
{
    public interface IRentcastService
    {
        Task<PropertyDetails?> GetProperty(string propertyID);


    }
}
