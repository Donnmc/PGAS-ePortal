namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class ip_phone_directory
    {
        public int? id { get; set; }
        public int? office_id { get; set; }
        public int? line_id { get; set; }
        public int? area_id { get; set; }
        public string? office_area { get; set; }
        public string? line_number { get; set; }
        public virtual ip_phone_directory_area? area { get; set; }
        public virtual ip_phone_directory_line? line { get; set; }
        public virtual ip_phone_directory_office? office { get; set; }
    }
}