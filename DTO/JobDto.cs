namespace JobPortal.DTO;

public class JobDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    // Extra properties
    public string EmployerId { get; set; }
}