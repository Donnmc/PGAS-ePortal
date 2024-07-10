namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class ip_phone_directory_area
    {
        public int? id { get; set; }

        public string? area { get; set; }

        public virtual ICollection<ip_phone_directory>? ip_phone_directory { get; set; } = new List<ip_phone_directory>();
    }
}