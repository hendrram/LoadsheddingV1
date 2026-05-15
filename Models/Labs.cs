namespace LoadsheddingV1.Models
{
    public class Labs
    {
        public int id { get; set; }
        public string? LabName { get; set; }
        public string? PC { get; set; }
        public bool OnOff {  get; set; } = false;
    }
}
