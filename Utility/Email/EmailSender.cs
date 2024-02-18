using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace JobPortal.Utility.Email;

public class EmailSender:IEmailSender
{
    
    private readonly string _email;
    private readonly int _mailSubmissionPort;
    private readonly string _password;
    private readonly string _smtpServerAddress;
    private readonly EmailSettings _emailSettings;
    public EmailSender(IConfiguration configuration)
    {
        _emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
    }
    public EmailSender(string email, string password, string smtpServerAddress, int mailSubmissionPort)
    {
        _email = email;
        _password = password;
        _smtpServerAddress = smtpServerAddress;
        _mailSubmissionPort = mailSubmissionPort;
    }
    
    public EmailSender(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings ?? throw new ArgumentNullException(nameof(emailSettings));
    }
    public  Response SendEmail(string emailTo, string mailSubject, string mailBody, bool isHtml = true)
    {
        try
        {
            var mailMessage = new MailMessage(_email, emailTo);
            mailMessage.Subject = mailSubject;
            mailMessage.Body = mailBody;
            mailMessage.IsBodyHtml = isHtml;
            var smtpClient = new SmtpClient(_smtpServerAddress, _mailSubmissionPort);
            smtpClient.Credentials = new NetworkCredential
            {
                UserName = _email,
                Password = _password
            };
            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);
            return ResponseFactory.Ok();
        }
        catch (Exception ex)
        {
            var err = new Error {Code = ex.Message, Description = ex.InnerException?.Message ?? ""};
            return ResponseFactory.Fail(err);
        }
    }
}