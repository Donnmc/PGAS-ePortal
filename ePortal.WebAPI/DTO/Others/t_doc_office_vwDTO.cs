namespace ePortal.WebAPI.DTO.Others
{
    public class t_doc_office_vwDTO
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public string? OfficeName { get; set; }
        public string? doctype { get; set; }
        public DateOnly? docdate { get; set; }
        public long? docid { get; set; }
    }
}
