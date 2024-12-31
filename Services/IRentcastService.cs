using RentcastBackend.Models.Property;
using RentcastBackend.Models.Tax;

namespace RentcastBackend.Interfaces
{
    public interface IRentcastService
    {
        Task<PropertyDetails? > GetProperty(string propertyID);


    }
}
