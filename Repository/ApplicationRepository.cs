using JobPortal.Data;
using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Repository.InterfaceRepository;
using JobPortal.Utility;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Repository;

public class ApplicationRepository:IApplicationRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _db = dbContext;
        _userManager = userManager;

    }
    public async Task<Response<ApplicationResponse>> AddApplication(ApplicationDto application)
    {
        try
        {
            if (application.ApplicantId == null)
            {
                return ResponseFactory.Fail<ApplicationResponse>(ErrorsList.CannotFindUser);
            }

            var applicantUser = await _userManager.FindByIdAsync(application.ApplicantId);

            if (applicantUser == null) return ResponseFactory.Fail<ApplicationResponse>(ErrorsList.CannotFindUser);
            var job = await _db.Jobs.FindAsync(application.JobId);
            if (job == null) return ResponseFactory.Fail<ApplicationResponse> (new Error{Description = "can't find the job" ,Code = "no such job with specific id"});

            var newApplication = new Application()
            {
                JobId = application.JobId,
                Job = job,
                ApplicantId = application.ApplicantId ,
                AppliedDate = application.AppliedDate,
                Applicant = applicantUser,
                CoverLetter = application.CoverLetter
            };
            await _db.Applications.AddAsync(newApplication);
            await _db.SaveChangesAsync();
            var data = new ApplicationResponse()
            {
                Id = newApplication.Id,
                JobId = newApplication.JobId,
                CoverLetter = application.CoverLetter,
                AppliedDate = newApplication.AppliedDate,
                ApplicantId = newApplication.ApplicantId
            };
            return ResponseFactory.Ok(data);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<ApplicationResponse>(ex);
        }
    }

    public async Task<Response<ApplicationResponse>> UpdateApplication(int applicationId, string coverLetter)
    {
        try
        {
            var application = await _db.Applications.FindAsync(applicationId);

            if (application == null) return ResponseFactory.Fail<ApplicationResponse> (new Error{Description = "can't find the application" ,Code = "no such application with specific id"});
            application.CoverLetter =coverLetter;
            await _db.SaveChangesAsync();
            var data = new ApplicationResponse()
            {
                Id = application.Id,
                JobId = application.JobId,
                CoverLetter = application.CoverLetter,
                AppliedDate = application.AppliedDate,
                ApplicantId = application.ApplicantId
            };
            return ResponseFactory.Ok(data);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<ApplicationResponse>(ex);
        }
    }

    public async Task<Response<IEnumerable<ApplicationResponse>>> GetAllApplicationByJobId(int jobId)
    {
        var job = await _db.Jobs.FindAsync(jobId);
        if (job == null) return ResponseFactory.Fail<IEnumerable<ApplicationResponse>> (new Error{Description = "can't find the job" ,Code = "no such job with specific id"});
        var applicationsJob= _db.Applications
            .Where(app => app.JobId == jobId).Select(app=> new ApplicationResponse()
            {
                Id = app.Id,
                JobId = app.JobId,
                CoverLetter = app.CoverLetter,
                AppliedDate = app.AppliedDate,
                ApplicantId = app.ApplicantId
            })
            .ToList();
        if (applicationsJob.Count == 0)
            ResponseFactory.Fail<IEnumerable<ApplicationResponse>>(new Error
                { Description = "no applications for this job", Code = "No application found. No application has been created yet for this job." });
       return ResponseFactory.Ok<IEnumerable<ApplicationResponse>>(applicationsJob);
    }

    public async Task<Response<ApplicationResponse>> GetApplicationById(int applicationId)
    {
        try
        {
            var application = await _db.Applications.FindAsync(applicationId);
            if (applicationId == null) return ResponseFactory.Fail<ApplicationResponse> (new Error{Description = "can't find the application" ,Code = "no such application with specific id"});

            var data = new ApplicationResponse()
            {
                Id = application.Id,
                JobId = application.JobId,
                CoverLetter = application.CoverLetter,
                AppliedDate = application.AppliedDate,
                ApplicantId = application.ApplicantId
            };
            return ResponseFactory.Ok(data);
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<ApplicationResponse>(ex);
        }
    }

    public async Task<Response> DeleteApplication(int applicationId)
    {
        try
        {
            var application = await _db.Applications.FindAsync(applicationId);
            if (application == null) ResponseFactory.Fail(new Error{Description = "can't find the application" ,Code = "no such application with specific id"}) ;
            _db.Applications.Remove(application);
            await _db.SaveChangesAsync();

            return ResponseFactory.Ok();
        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException(ex);
        }
    }
}