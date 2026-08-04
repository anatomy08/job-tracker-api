namespace JobTracker.Api.DTOs;

public class CreateJobApplicationDto
{
    public string CompanyName { get; set; } = string.Empty;

    public string PositionTitle { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}