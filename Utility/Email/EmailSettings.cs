﻿namespace JobPortal.Utility.Email;

public class EmailSettings
{
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public string ConfirmationLinkBase { get; set; } 
}