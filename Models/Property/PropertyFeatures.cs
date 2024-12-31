namespace RentcastBackend.Models.Property
{
    public class PropertyFeatures
    {
        public string ArchitectureType { get; set; }
        public bool Cooling { get; set; }
        public string CoolingType { get; set; }
        public string ExteriorType { get; set; }
        public bool Fireplace { get; set; }
        public string FireplaceType { get; set; }
        public int FloorCount { get; set; }
        public string FoundationType { get; set; }
        public bool Garage { get; set; }
        public int GarageSpaces { get; set; }
        public string GarageType { get; set; }
        public bool Heating { get; set; }
        public string HeatingType { get; set; }
        public bool Pool { get; set; }
        public string PoolType { get; set; }
        public string RoofType { get; set; }
        public int RoomCount { get; set; }
        public int UnitCount { get; set; }
        public string ViewType { get; set; }
    }
}
