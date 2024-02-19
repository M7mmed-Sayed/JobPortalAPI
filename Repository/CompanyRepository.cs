using JobPortal.Data;
using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Repository.InterfaceRepository;
using JobPortal.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CompanyRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _db = dbContext;
        _userManager = userManager;

    }
    public async Task<Response<Company>> AddCompany(CompanyDto company)
    {
         try
         {
             if (company.UserId == null)
             {
                 return ResponseFactory.Fail<Company>(ErrorsList.CannotFindUser);
             }

             var creatorUser = await _userManager.FindByIdAsync(company.UserId);

             if (creatorUser == null) return ResponseFactory.Fail<Company>(ErrorsList.CannotFindUser);

             var newCompany = new Company()
             {
                 Description = company.Description,
                 Name = company.Name,
                 Website = company.Website,
                 UserId = company.UserId
             };
             await _db.Companies.AddAsync(newCompany);
             await _db.SaveChangesAsync();
             return ResponseFactory.Ok(newCompany);
         }
         catch (Exception ex)
         {
             return ResponseFactory.FailFromException<Company>(ex);
         }
    }

    public async Task<Response<Company>> UpdateCompany(int companyId, CompanyDto companyDto)
    {
        try
        {
            var company = await _db.Companies.FindAsync(companyId);

            if (company == null) return ResponseFactory.Fail<Company> (new Error{Description = "can't find the company" ,Code = "no such company with specific id"});
            company.Description = companyDto.Description;
            company.Name = companyDto.Name;
            company.Website = companyDto.Website;
            await _db.SaveChangesAsync();
            return ResponseFactory.Ok(company);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<Company>(ex);
        }
    }

    public async Task<Response<IEnumerable<CompanyResponse>>> GetAllCompanies()
    {
        var companies = await _db.Companies
            .Select(c => new CompanyResponse
            {
                Name = c.Name,
                Id = c.Id,
                Description = c.Description,
                Website = c.Website
            })
            .ToListAsync(); 
        return ResponseFactory.Ok<IEnumerable<CompanyResponse>>(companies);
    }

    public async Task<Response<CompanyResponse>> GetCompanyById(int companyId)
    {
        try
        {
            var company = await _db.Companies.FindAsync(companyId);
            var responseDate = new CompanyResponse()
            {
                Name = company.Name,
                Id = company.Id,
                Description = company.Description,
                Website = company.Website
            };
            return company == null ?
                ResponseFactory.Fail<CompanyResponse> (new Error{Description = "can't find the company" ,Code = "no such company with specific id"}) : 
                ResponseFactory.Ok(responseDate);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<CompanyResponse>(ex);
        }
    }

    public async Task<Response> DeleteCompany(int companyId)
    {
        try
        {
            var company = await _db.Companies.FindAsync(companyId);
            if (company == null) ResponseFactory.Fail<Company> (new Error{Description = "can't find the company" ,Code = "no such company with specific id"}) ;
            _db.Companies.Remove(company);
            await _db.SaveChangesAsync();

            return ResponseFactory.Ok();
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException(ex);
        }
    }
}