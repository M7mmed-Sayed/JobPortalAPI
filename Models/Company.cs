namespace JobPortal.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Website { get; set; }

    // Navigation properties
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

}