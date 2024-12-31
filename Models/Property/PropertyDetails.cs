using RentcastBackend.Models.History;
using RentcastBackend.Models.Owner;
using RentcastBackend.Models.Tax;

namespace RentcastBackend.Models.Property
{
    public class PropertyDetails
    {
        public Property Property { get; set; }
        public HOA HOA { get; set; }
        public PropertyFeatures Features { get; set; }
        public Dictionary<string, TaxAssessment> TaxAssessments { get; set; }
        public Dictionary<string, PropertyTax> PropertyTaxes { get; set; }
        public Dictionary<string, HistoryEvent> History { get; set; }
        public OwnerModel Owner { get; set; }
        public bool OwnerOccupied { get; set; }

    }
}
