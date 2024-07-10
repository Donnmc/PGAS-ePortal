namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class ip_phone_directory_line
    {
        public int? id { get; set; }

        public string? line { get; set; }

        public virtual ICollection<ip_phone_directory>? ip_phone_directory { get; set; } = new List<ip_phone_directory>();
    }
}