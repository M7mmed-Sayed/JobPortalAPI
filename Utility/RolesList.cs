namespace JobPortal.Utility;

public class RolesList
{
    public const string Administrator = "Admin";
    public const string JobSeeker = "JobSeeker";
    public const string Employer = "Employer";
   
    public static List<string> Roles { get; private set; } = new List<string>() { Administrator, JobSeeker, Employer};

}