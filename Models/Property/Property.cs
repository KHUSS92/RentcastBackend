namespace RentcastBackend.Models.Property
{
    public class Property
    {
        public string Id { get; set; }
        public string FormattedAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PropertyType { get; set; }
        public int Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int SquareFootage { get; set; }
        public int LotSize { get; set; }
        public int YearBuilt { get; set; }
        public string AssessorID { get; set; }
        public string LegalDescription { get; set; }
        public string Subdivision { get; set; }
        public string Zoning { get; set; }
        public DateTime LastSaleDate { get; set; }
        public decimal LastSalePrice { get; set; }
    }
}
