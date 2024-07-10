namespace PGAS.WebAPI.DTO.PMIS.View
{
    public partial class eportal_docs_viewDTO
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public string? OfficeName { get; set; }
        public string? doctype { get; set; }
        public DateTime? docdate { get; set; }
        public long? docid { get; set; }
    }
}