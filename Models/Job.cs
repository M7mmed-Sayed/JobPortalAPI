namespace JobPortal.Models;

public class Job
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    // Extra properties
    public string EmployerId { get; set; }
    public ApplicationUser Employer { get; set; }
}