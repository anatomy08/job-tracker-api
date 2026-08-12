using JobTracker.Api.Data;
using JobTracker.Api.DTOs;
using JobTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace JobTracker.Api.Controllers;

[ApiController] // This attribute indicates that the controller responds to web API requests
[Route("api/[controller]")] // The [controller] token becomes "JobApplications".
public class JobApplicationsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHostEnvironment _environment;

    public JobApplicationsController(AppDbContext context, IHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }


    // PRACTICE 13: CREATE A NEW GET ENDPOINT FILTERS JOBAPPLICATION BY STATUS AND RETURN A LIST OF DTO'S
    [ProducesResponseType(typeof(List<JobApplicationDto>), StatusCodes.Status200OK)]
    [HttpGet("filter")] // NEW END POINT  : GET /api/jobapplications/filter
    public async Task<ActionResult<List<JobApplicationDto>>> GetFiltered([FromQuery] string? status)
    {
        // 1. Start with the JobApplications database query.
        IQueryable<JobApplication> query = _context.JobApplications;
           

        // 2. If status was provided, filter the query.
        // Make the comparison case-insensitive.

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(application => application.Status.ToLower() == status.ToLower());
        }

      //  application.Status.ToLower() == status.ToLower()
        // 3. Map every remaining JobApplication to JobApplicationDto.

        List<JobApplicationDto> applicationdtos = await query
            .Select(application => new JobApplicationDto
            {
                CompanyName = application.CompanyName,
                Status = application.Status
            }).ToListAsync();

        // 4. Execute the query asynchronously and return 200 OK.

        return Ok(applicationdtos);
    }

    // PRACTICE 10: GET A DTO BY ID
    [ProducesResponseType(typeof(JobApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("dto/{id:int}")]
    public async Task<ActionResult<JobApplicationDto>> GetByIdDto(int id)
    {
        JobApplication? application = await _context.JobApplications
            .FirstOrDefaultAsync(application => application.Id == id);

        if (application is null)
        {
            return NotFound();
        }

        JobApplicationDto dto = new JobApplicationDto
        {
            CompanyName = application.CompanyName,
            Status = application.Status
        };

        return Ok(dto);
    }

    // GET ALL DATA RECORDS
    [ProducesResponseType(typeof(List<JobApplication>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<List<JobApplication>> Get()
    {
        return await _context.JobApplications.ToListAsync();
    }

    // GET A SPECIFIC DATA RECORD BY ID
    [ProducesResponseType(typeof(JobApplication), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<JobApplication>> GetById(int id)
    {
        JobApplication? application = await _context.JobApplications.FindAsync(id);

        return application != null ? Ok(application) : NotFound();
    }

    // CREATE NEW DATA
    [ProducesResponseType(typeof(JobApplicationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<JobApplicationDto>> Post([FromBody] CreateJobApplicationDto request)
    {
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


        // Map the saved application to JobApplicationDto.
        JobApplicationDto jobApplicationDto = new JobApplicationDto
        {
            CompanyName = application.CompanyName,
            Status = application.Status
        };


        //return Created($"/api/jobapplications/{application.Id}", application);

        return CreatedAtAction(nameof(GetByIdDto), new { id = application.Id }, jobApplicationDto);

    }

    // Practice 12
    [ProducesResponseType(typeof(JobApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<JobApplicationDto>> Put(int id, [FromBody] UpdateJobApplicationDto request)
    {
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

        JobApplicationDto applicationDto = new JobApplicationDto
        {
            CompanyName = application.CompanyName,
            Status = application.Status
        };

        return Ok(applicationDto);
    }

    // HARD DELETE DATA PERMANENTLY
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
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
