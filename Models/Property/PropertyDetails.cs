namespace RentcastBackend.Models.Property
{
    public class PropertyDetails
    {
        public string Id { get; set; }
        public string FormattedAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string PropertyType { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? SquareFootage { get; set; }
        public int? LotSize { get; set; }
        public int? YearBuilt { get; set; }
        public string AssessorID { get; set; }
        public string LegalDescription { get; set; }
        public string Subdivision { get; set; }
        public string Zoning { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public decimal? LastSalePrice { get; set; }
        public HOA Hoa { get; set; }
        public PropertyFeatures Features { get; set; }
        public Dictionary<string, TaxAssessment> TaxAssessments { get; set; }
        public Dictionary<string, PropertyTax> PropertyTaxes { get; set; }
        public Dictionary<string, HistoryEvent> History { get; set; }
        public OwnerModel Owner { get; set; }
        public bool? OwnerOccupied { get; set; }
    }

    public class HOA
    {
        public decimal? Fee { get; set; }
    }

    public class PropertyFeatures
    {
        public string ArchitectureType { get; set; }
        public bool? Cooling { get; set; }
        public string CoolingType { get; set; }
        public string ExteriorType { get; set; }
        public bool? Fireplace { get; set; }
        public string FireplaceType { get; set; }
        public int? FloorCount { get; set; }
        public string FoundationType { get; set; }
        public bool? Garage { get; set; }
        public int? GarageSpaces { get; set; }
        public string GarageType { get; set; }
        public bool? Heating { get; set; }
        public string HeatingType { get; set; }
        public bool? Pool { get; set; }
        public string PoolType { get; set; }
        public string RoofType { get; set; }
        public int? RoomCount { get; set; }
        public int? UnitCount { get; set; }
        public string ViewType { get; set; }
    }

    public class TaxAssessment
    {
        public int? Year { get; set; }
        public decimal? Value { get; set; }
        public decimal? Land { get; set; }
        public decimal? Improvements { get; set; }
    }

    public class PropertyTax
    {
        public int? Year { get; set; }
        public decimal? Total { get; set; }
    }

    public class HistoryEvent
    {
        public string Event { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Price { get; set; }
    }

    public class OwnerModel
    {
        public List<string> Names { get; set; }
        public string Type { get; set; }
        public MailingAddress MailingAddress { get; set; }
    }

    public class MailingAddress
    {
        public string Id { get; set; }
        public string FormattedAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

}
