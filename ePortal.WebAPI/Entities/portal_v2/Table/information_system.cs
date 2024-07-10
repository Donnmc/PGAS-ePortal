namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class information_system
    {
        public int id { get; set; }

        public int? information_system_cluster_id { get; set; }

        public string? name { get; set; }

        public string? abbreviation { get; set; }

        public string? icon { get; set; }

        public string? link { get; set; }

        public string? platform { get; set; }

        public DateOnly? date_created { get; set; }

        public bool? active { get; set; }

        public virtual information_system_cluster? information_system_cluster { get; set; }
    }
}