namespace RentcastBackend.Models.History
{
    public class HistoryEvent
    {
        public string Event { get; set; }
        public DateTime Date { get; set; }
        public decimal? Price { get; set; }
    }
}
