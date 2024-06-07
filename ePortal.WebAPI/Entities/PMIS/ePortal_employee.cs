namespace ePortal.WebAPI.Entities;

public partial class ePortal_employee
{
    public long? eid { get; set; }
    public string? SwipeID { get; set; }
    public string? OfficeName { get; set; }
    public string? OfficeAbbr { get; set; }
    public string? EmployeeName { get; set; }
    public string? Position { get; set; }
    public int? SG { get; set; }
    public string? Status { get; set; }
    public bool? isactive { get; set; }
}