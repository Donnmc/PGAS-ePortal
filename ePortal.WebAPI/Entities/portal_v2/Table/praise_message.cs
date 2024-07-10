namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class praise_message
    {
        public int id { get; set; }
        public long? from_eid { get; set; }
        public long? to_eid { get; set; }
        public string? message { get; set; }
        public int? stars { get; set; }
        public DateTime? date { get; set; }
        public bool? archive { get; set; }
    }
}