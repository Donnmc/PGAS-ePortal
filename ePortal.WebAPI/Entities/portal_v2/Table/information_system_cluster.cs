namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class information_system_cluster
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? abbreviation { get; set; }
        public string? icon { get; set; }
        public bool? active { get; set; }
        public virtual ICollection<information_system>? information_system { get; set; } = new List<information_system>();
    }
}