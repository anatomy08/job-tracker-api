namespace JobTracker.Api.DTOs
{
    public class PagedJobApplicationsDto
    {
        public List<JobApplicationDto> Items { get; set; } = new();

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }
    }
}
