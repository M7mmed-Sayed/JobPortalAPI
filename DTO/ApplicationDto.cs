namespace JobPortal.DTO;

public class ApplicationDto
{
    public int JobId { get; set; }

    public string ApplicantId { get; set; }

    public DateTime AppliedDate { get; set; }
    public string CoverLetter { get; set; }
}