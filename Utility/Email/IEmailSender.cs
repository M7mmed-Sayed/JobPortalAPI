namespace JobPortal.Utility.Email;

public interface IEmailSender
{
    Response SendEmail(string emailTo, string mailSubject, string mailBody, bool isHtml = true);
}