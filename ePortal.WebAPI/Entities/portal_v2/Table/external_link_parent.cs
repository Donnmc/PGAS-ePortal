namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class external_link_parent
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? icon { get; set; }
        public string? link { get; set; }
        public int? order { get; set; }
        public DateTime? date_created { get; set; }
        public bool? active { get; set; }
        public virtual ICollection<external_link_child>? external_link_child { get; set; } = new List<external_link_child>();
    }
}