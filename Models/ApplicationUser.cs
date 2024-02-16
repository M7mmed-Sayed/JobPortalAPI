using JobPortal.Utility;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models;

public class ApplicationUser : IdentityUser
{
    //main Information
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }

    // Contact Information
    public string PhoneNumber2 { get; set; }
    public string LinkedInProfile { get; set; }
    public string GitHubProfile { get; set; }
    
    // Job Seeker Information
    public string ResumeFileName { get; set; }
    public string CoverLetter { get; set; }
    public bool IsJobSeeker { get; set; }

    // Employer Information
    public string CompanyName { get; set; }
    public string CompanyWebsite { get; set; }
    public bool IsEmployer { get; set; }

    // Additional Properties (customize based on your needs)
    public DateTime? Birthdate { get; set; }
    public Gender Gender { get; set; }
    
}