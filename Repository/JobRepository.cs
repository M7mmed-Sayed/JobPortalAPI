

using JobPortal.Data;
using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Repository;

public class JobRepository:IJobRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public JobRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _db = dbContext;
        _userManager = userManager;

    }

    public async Task<Response<Job>> AddJob(JobDto job)
    {
        try
        {
            if (job.EmployerId == null)
            {
                return ResponseFactory.Fail<Job>(ErrorsList.CannotFindUser);
            }

            var creatorUser = await _userManager.FindByIdAsync(job.EmployerId);

            if (creatorUser == null) return ResponseFactory.Fail<Job>(ErrorsList.CannotFindUser);

            var newJob = new Job()
            {
                Description = job.Description,
                Title = job.Title,
                Location=job.Location,
                PostedDate = job.PostedDate,
                ExpiryDate = job.ExpiryDate
            };
            await _db.Jobs.AddAsync(newJob);
            await _db.SaveChangesAsync();
            return ResponseFactory.Ok(newJob);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<Job>(ex);
        }
    }

    public async Task<Response<Job>> UpdateJob(int jobId, JobDto jobDto)
    {
        try
        {
            var job = await _db.Jobs.FindAsync(jobId);

            if (job == null) return ResponseFactory.Fail<Job> (new Error{Description = "can't find the job" ,Code = "no such job with specific id"});
            job.Description = jobDto.Description;
            job.Title = jobDto.Title;
            job.Location = jobDto.Location;
            job.ExpiryDate = jobDto.ExpiryDate;
            await _db.SaveChangesAsync();
            return ResponseFactory.Ok(job);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<Job>(ex);
        }
    }

    public async Task<Response<IEnumerable<JobResponse>>> GetAllJobs()
    {
        var jobs = await _db.Jobs
            .Select(job => new JobResponse()
            {
                Description = job.Description,
                Title = job.Title,
                Location=job.Location,
                PostedDate = job.PostedDate,
                ExpiryDate = job.ExpiryDate
            })
            .ToListAsync();
        return jobs.Count==0 ? ResponseFactory.Fail<IEnumerable<JobResponse>> (new Error{Description = "no jobs" ,Code = "No jobs found. No job has been created yet."}) : ResponseFactory.Ok<IEnumerable<JobResponse>>(jobs);
    }

    public async Task<Response<JobResponse>> GetJobById(int jobId)
    {
        try
        {
            var job = await _db.Jobs.FindAsync(jobId);
            var responseDate = new JobResponse()
            {
                Description = job.Description,
                Title = job.Title,
                Location=job.Location,
                PostedDate = job.PostedDate,
                ExpiryDate = job.ExpiryDate
            };
            return job == null ?
                ResponseFactory.Fail<JobResponse> (new Error{Description = "can't find the job" ,Code = "no such job with specific id"}) : 
                ResponseFactory.Ok(responseDate);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<JobResponse>(ex);
        }
    }

    public async Task<Response> DeleteJob(int jobId)
    {
        try
        {
            var job = await _db.Jobs.FindAsync(jobId);
            if (job == null) ResponseFactory.Fail(new Error{Description = "can't find the job" ,Code = "no such job with specific id"}) ;
            _db.Jobs.Remove(job);
            await _db.SaveChangesAsync();

            return ResponseFactory.Ok();
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException(ex);
        }
        
    }
}