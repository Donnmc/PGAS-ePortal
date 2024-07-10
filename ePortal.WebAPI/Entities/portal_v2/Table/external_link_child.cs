namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class external_link_child
    {
        public int? id { get; set; }
        public int? parent_id { get; set; }
        public string? name { get; set; }
        public string? link { get; set; }
        public string? icon { get; set; }
        public int? order { get; set; }
        public DateTime? date_created { get; set; }
        public bool? active { get; set; }
        public virtual external_link_parent? parent { get; set; }
    }
}