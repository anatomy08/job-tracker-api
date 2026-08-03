using JobTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace JobTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobApplicationsController : ControllerBase
{
    [HttpGet]
    public List<JobApplication> Get()
    {
        string[] Companynames = new string[] { "Ubisoft", "Microsoft", "Google", "Amazon", "Facebook" };

        int numberOfApplications = Companynames.Length;

        List<JobApplication> ListOfApplications = new List<JobApplication>();

        for (int i = 0; i < numberOfApplications; i++)
        {
            JobApplication application = new JobApplication
            {

                Id = 1 + i,
                CompanyName = Companynames[i],
                PositionTitle = ChosenPositionTitle(),
                Status = NewStatus()
            };

            ListOfApplications.Add(application);
        }

        return ListOfApplications;
    }

    static string ChosenPositionTitle()
    {
        string[] PositionTitles = new string[] { "Unity Developer", "Backend Developer", "Frontend Developer", "Fullstack Developer", "Game Designer" };
        int index = Random.Shared.Next(PositionTitles.Length);
        string chosenPositionTitle = PositionTitles[index];

        return chosenPositionTitle;
    }

    static string NewStatus()
    {
        string[] Statuses = new string[] { "applied", "interviewing", "offered", "rejected" };
        int index = Random.Shared.Next(Statuses.Length);
        string chosenStatus = Statuses[index];

        return chosenStatus;
    }
}