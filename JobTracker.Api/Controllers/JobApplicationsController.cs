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
    private readonly IHostEnvironment _environment;


    public JobApplicationsController(AppDbContext context, IHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET ALL DATA RECORD
    [ProducesResponseType(typeof(List<JobApplication>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<List<JobApplication>> Get()
    {
        return await _context.JobApplications.ToListAsync();
    }


    // GETS THE SPECIFIC ID DATA
    [ProducesResponseType(typeof(JobApplication), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<JobApplication>> GetById(int id)
    {
        JobApplication? application = await _context.JobApplications.FindAsync(id);


        return application != null ? Ok(application) : NotFound();
    }

    // CREATE NEW DATA
    [ProducesResponseType(typeof(JobApplication), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<JobApplication>> Post(
    [FromBody] CreateJobApplicationDto request)
    {
        // AUTHENTICATION FOR POST
        if (_environment.IsProduction())
        {
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                message = "This public portfolio API is read-only. Only GET requests are available."
            });
        }

        JobApplication application = new JobApplication
        {
            CompanyName = request.CompanyName,
            PositionTitle = request.PositionTitle,
            Status = request.Status,
            DateApplied = request.DateApplied!.Value,
            JobUrl = request.JobUrl,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync();

        return Created($"/api/jobapplications/{application.Id}", application);
    }

    // EDIT THE DATA RECORD OF SPECIFIC ID
    [ProducesResponseType(typeof(JobApplication), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    [HttpPut("{id:int}")]
    public async Task<ActionResult<JobApplication>> Put(int id,
    [FromBody] UpdateJobApplicationDto request)
    {

        // AUTHENTICATION FOR PUT
        if (_environment.IsProduction())
        {
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                message = "This public portfolio API is read-only. Only GET requests are available."
            });
        }

        JobApplication? application = await _context.JobApplications.FindAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        application.CompanyName = request.CompanyName;
        application.PositionTitle = request.PositionTitle;
        application.Status = request.Status;
        application.DateApplied = request.DateApplied!.Value;
        application.JobUrl = request.JobUrl;
        application.Notes = request.Notes;
        application.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(application);


    }


    // HARD DELETE DATA PERMANENTLY DELETE
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {

        // AUTHENTICATION FOR DELETE
        if (_environment.IsProduction())
        {
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                message = "This public portfolio API is read-only. Only GET requests are available."
            });
        }


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