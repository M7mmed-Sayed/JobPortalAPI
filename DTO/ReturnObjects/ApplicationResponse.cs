namespace JobPortal.DTO.ReturnObjects;

public class ApplicationResponse
{
    public int Id { get; set; }
    public int JobId { get; set; }

    public string ApplicantId { get; set; }

    public DateTime AppliedDate { get; set; }
    public string CoverLetter { get; set; }
}