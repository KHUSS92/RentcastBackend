namespace RentcastBackend.Models.Tax
{
    public class TaxAssessment
    {
        public int Year { get; set; }
        public decimal Value { get; set; }
        public decimal Land { get; set; }
        public decimal Improvements { get; set; }
    }
}
