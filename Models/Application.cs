namespace JobPortal.Models;

public class Application
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }

    public string ApplicantId { get; set; }
    public ApplicationUser Applicant { get; set; }

    public DateTime AppliedDate { get; set; }
    public string CoverLetter { get; set; }
}