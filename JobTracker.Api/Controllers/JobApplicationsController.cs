using JobTracker.Api.Data;
using JobTracker.Api.DTOs;
using JobTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace JobTracker.Api.Controllers;

[ApiController] // This attribute indicates that the controller responds to web API requests
[Route("api/[controller]")] // This attribute defines the route template for the controller. The [controller] token is replaced with the name of the controller, which in this case is "JobApplications". So, the route for this controller will be "api/jobapplications".
public class JobApplicationsController : ControllerBase
{
    private readonly AppDbContext _context; // This is a private field that holds a reference to the application's database context. The database context is used to interact with the database.

    public JobApplicationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<JobApplication>> Get()
    {
        return await _context.JobApplications.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<JobApplication>> GetById(int id)
    {
        JobApplication? application = await _context.JobApplications.FindAsync(id);


        return application != null ? Ok(application) : NotFound();
    }


    [HttpPost]
    public async Task<ActionResult<JobApplication>> Post(
    [FromBody] CreateJobApplicationDto request)
    {
        JobApplication application = new JobApplication
        {
            CompanyName = request.CompanyName,
            PositionTitle = request.PositionTitle,
            Status = request.Status
        };

        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync();

        return Created($"/api/jobapplications/{application.Id}", application);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<JobApplication>> Put(int id,
    [FromBody] UpdateJobApplicationDto request)
    {
        JobApplication? application = await _context.JobApplications.FindAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        application.CompanyName = request.CompanyName;
        application.PositionTitle = request.PositionTitle;
        application.Status = request.Status;

        await _context.SaveChangesAsync();

        return Ok(application);


    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        JobApplication? application = await _context.JobApplications.FindAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        _context.JobApplications.Remove(application);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}