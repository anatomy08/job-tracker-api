using System.Net;
using System.Net.Http.Json;

namespace JobTracker.Api.Tests;

// Shares one in-memory API test server for this test class.
public class JobApplicationsApiTests : IClassFixture<JobTrackerApiFactory>
{
    private readonly HttpClient _client;

    public JobApplicationsApiTests(JobTrackerApiFactory factory)
    {
        // Creates a client that sends requests to the in-memory API server.
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_WhenDateAppliedIsMissing_ReturnsBadRequest()
    {
        // Arrange: create intentionally invalid frontend JSON.
        // DateApplied is required, but we do not send it.
        var request = new
        {
            companyName = "Nintendo",
            positionTitle = "Gameplay Programmer",
            status = "Applied"
        };

        // Act: send POST to the real API endpoint.
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/api/jobapplications",
            request);

        // Assert: validation must reject the request with HTTP 400.
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_WhenStatusIsInvalid_ReturnsBadRequest()
    {
        // Arrange: Status is required but must be one of the allowed values.
        // "Pending" is not allowed by the API RegularExpression rule.
        var request = new
        {
            companyName = "Nintendo",
            positionTitle = "Gameplay Programmer",
            status = "Pending", // WE DONT HAVE PENDING AS A VALID STATUS, SO THIS SHOULD FAIL
            dateApplied = "2026-08-06"
        };

        // Act: send POST to the real API endpoint.
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/api/jobapplications",
            request);

        // Assert: validation must reject the invalid status with HTTP 400.
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNotFound()
    {
        // Arrange: use an ID that is not in the empty in-memory test database.
        const int missingId = 999999;

        // Act: ask the real API endpoint for that missing record.
        HttpResponseMessage response = await _client.GetAsync(
            $"/api/jobapplications/{missingId}");

        // Assert: the API must return HTTP 404 instead of an internal server error.
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_WhenRequestIsValid_ReturnsCreated()
    {
        // Arrange: create valid frontend JSON that meets every DTO rule.
        var request = new
        {
            companyName = "Nintendo",
            positionTitle = "Gameplay Programmer",
            status = "Applied",
            dateApplied = "2026-08-06",
            jobUrl = "https://www.nintendo.com/careers",
            notes = "Created by an automated test"
        };

        // Act: send POST to the real API running with the temporary test database.
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "/api/jobapplications",
            request);

        // Assert: valid input must create a record and return HTTP 201.
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}