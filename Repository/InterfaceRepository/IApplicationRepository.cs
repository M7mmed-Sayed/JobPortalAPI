using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Utility;

namespace JobPortal.Repository.InterfaceRepository;

public interface IApplicationRepository
{
    Task<Response<ApplicationResponse>> AddApplication(ApplicationDto application);
    Task<Response<ApplicationResponse>> UpdateApplication(int applicationId, string coverLetter);
    Task<Response<IEnumerable<ApplicationResponse>>> GetAllApplicationByJobId(int jobId);
    Task<Response<ApplicationResponse>> GetApplicationById(int applicationId);
    Task<Response> DeleteApplication(int applicationId);
}