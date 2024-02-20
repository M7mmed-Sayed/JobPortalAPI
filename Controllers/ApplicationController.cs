using JobPortal.DTO;
using JobPortal.Repository;
using JobPortal.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApplicationController : Controller
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationController(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllApllicationsByJobID([FromQuery] int jobId )
    {
        var result = await _applicationRepository.GetAllApplicationByJobId(jobId);
        return result.Succeeded ? Ok(result) : Unauthorized(result.Errors);

    }

    [HttpGet("{applicationId:int}")]
    public async Task<IActionResult> GetApplicationById(int applicationId)
    {
        var result = await _applicationRepository.GetApplicationById(applicationId);
        return result.Succeeded ? Ok(result) : Unauthorized(result.Errors);

    }

    [HttpPost("add-application")]
    public async Task<IActionResult> AddApplication([FromBody] ApplicationDto applicationDto)
    {
        var result= await _applicationRepository.AddApplication(applicationDto);
        return result.Succeeded ? Ok(result) : Unauthorized(result.Errors);

    }

    [HttpPut("{applicationId:int}")]
    public async Task<IActionResult> UpdateApplication(int applicationId, [FromBody] string coverLetter)
    {
        var result=await _applicationRepository.UpdateApplication(applicationId,coverLetter);
        return result.Succeeded ? Ok(result) : Unauthorized(result.Errors);
    }

    [HttpDelete("{applicationId:int}")]
    public async Task<IActionResult> DeleteApplication(int applicationId)
    {
        var result=  await _applicationRepository.DeleteApplication(applicationId);
        return result.Succeeded ? Ok(result) : Unauthorized(result.Errors);

    }
  
}