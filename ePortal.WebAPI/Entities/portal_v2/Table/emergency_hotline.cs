namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class emergency_hotline
    {
        public int? id { get; set; }
        public int? office_id { get; set; }
        public int? line_id { get; set; }
        public string? area { get; set; }
        public string? mobile_number { get; set; }
        public virtual emergency_hotline_line? line { get; set; }
        public virtual emergency_hotline_office? office { get; set; }
    }
}