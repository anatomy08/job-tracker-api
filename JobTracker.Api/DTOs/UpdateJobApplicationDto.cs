using System.ComponentModel.DataAnnotations;

namespace JobTracker.Api.DTOs;

public class UpdateJobApplicationDto
{
    [Required]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    public string PositionTitle { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(Applied|Interviewing|Offered|Rejected)$")]
    public string Status { get; set; } = string.Empty;

    [Required]
    public DateOnly? DateApplied { get; set; }

    [Url]
    public string? JobUrl { get; set; }

    public string? Notes { get; set; }
}