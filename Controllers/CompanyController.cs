using JobPortal.DTO;
using JobPortal.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CompanyController : Controller
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCompanies()
    {
        var result = await _companyRepository.GetAllCompanies();
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(int id)
    {
        var result = await _companyRepository.GetCompanyById(id);
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpPost("add-company")]
    public async Task<IActionResult> AddCompany([FromBody] CompanyDto company)
    {
       var result= await _companyRepository.AddCompany(company);
       if (!result.Succeeded)
       {
           return Unauthorized(result.Errors);
       }

       return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDto company)
    {
        
        var result=await _companyRepository.UpdateCompany(id,company);
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
      var result=  await _companyRepository.DeleteCompany(id);
        if (!result.Succeeded)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }
}
