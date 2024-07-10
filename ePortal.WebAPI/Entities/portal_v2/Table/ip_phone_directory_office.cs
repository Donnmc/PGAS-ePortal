namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class ip_phone_directory_office
    {
        public int? id { get; set; }

        public string? office { get; set; }

        public string? office_abbreviation { get; set; }

        public virtual ICollection<ip_phone_directory>? ip_phone_directory { get; set; } = new List<ip_phone_directory>();
    }
}