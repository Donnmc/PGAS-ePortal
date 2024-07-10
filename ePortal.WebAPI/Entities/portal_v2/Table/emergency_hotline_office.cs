namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class emergency_hotline_office
    {
        public int? id { get; set; }
        public string? office { get; set; }
        public string? office_abbreviation { get; set; }
        public virtual ICollection<emergency_hotline>? emergency_hotline { get; set; } = new List<emergency_hotline>();
    }
}