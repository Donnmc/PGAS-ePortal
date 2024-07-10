namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class emergency_hotline_line
    {
        public int? id { get; set; }
        public string? line { get; set; }
        public virtual ICollection<emergency_hotline>? emergency_hotline { get; set; } = new List<emergency_hotline>();
    }
}