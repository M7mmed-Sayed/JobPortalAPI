using JobPortal.DTO;
using JobPortal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JobController : Controller
{
    // GET
    private readonly IJobRepository _jobRepository;

    public JobController(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        var result = await _jobRepository.GetAllJobs();
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobById(int id)
    {
        var result = await _jobRepository.GetJobById(id);
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpPost("add-company")]
    public async Task<IActionResult> AddCompany([FromBody] JobDto jobDto)
    {
        var result= await _jobRepository.AddJob(jobDto);
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(int id, [FromBody] JobDto jobDto)
    {
        
        var result=await _jobRepository.UpdateJob(id,jobDto);
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        var result=  await _jobRepository.DeleteJob(id);
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }
}