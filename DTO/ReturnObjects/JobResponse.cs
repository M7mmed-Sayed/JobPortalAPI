﻿namespace JobPortal.DTO.ReturnObjects;

public class JobResponse
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime PostedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}