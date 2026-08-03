namespace JobTracker.Api.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string PositionTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
