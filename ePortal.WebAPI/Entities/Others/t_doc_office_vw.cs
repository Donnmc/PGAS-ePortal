namespace ePortal.WebAPI.Entities;

public partial class t_doc_office_vw
{
    public string? title { get; set; }
    public string? description { get; set; }
    public string? OfficeName { get; set; }
    public string? doctype { get; set; }
    public DateOnly? docdate { get; set; }
    public long? docid { get; set; }
}