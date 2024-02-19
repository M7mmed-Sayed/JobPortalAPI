using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Utility;

namespace JobPortal.Repository;

public interface IJobRepository
{
    Task<Response<Job>> AddJob(JobDto job);
    Task<Response<Job>> UpdateJob(int jobId, JobDto jobDto);
    Task<Response<IEnumerable<JobResponse>>> GetAllJobs();
    Task<Response<JobResponse>> GetJobById(int jobId);
    Task<Response> DeleteJob(int jobId);
}