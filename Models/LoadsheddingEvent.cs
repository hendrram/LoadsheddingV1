namespace LoadsheddingV1.Models
{
    public class LoadsheddingEvent
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? AreaId { get; internal set; }

        public DateTime LastUpdated { get; set; }   
    }
}
