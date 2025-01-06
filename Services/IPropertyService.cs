using RentcastBackend.Models.Property;

namespace RentcastBackend.Services
{
    public interface IPropertyService
    {
        Task<PropertyDetails?> GetProperty(string propertyId);
    }
}
