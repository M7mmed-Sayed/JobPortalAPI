using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Utility;

namespace JobPortal.Repository.InterfaceRepository;

public interface ICompanyRepository
{
    Task<Response<Company>> AddCompany(CompanyDto company);
    Task<Response<Company>> UpdateCompany(int companyId, CompanyDto companyDto);
    Task<Response<IEnumerable<CompanyResponse>>> GetAllCompanies();
    Task<Response<CompanyResponse>> GetCompanyById(int companyId);
    Task<Response> DeleteCompany(int companyId);
}