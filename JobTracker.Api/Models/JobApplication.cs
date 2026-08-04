namespace JobTracker.Api.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string PositionTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public DateOnly DateApplied { get; set; }
        public string? JobUrl { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
