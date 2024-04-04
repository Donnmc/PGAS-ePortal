namespace ePortal.WebAPI.DTO
{
    public class information_system_clusterDTO
    {
        public int id { get; set; }

        public string name { get; set; } = string.Empty;

        public string abbreviation { get; set; } = string.Empty;

        public string icon { get; set; } = string.Empty;

        public bool? active { get; set; }
    }
}
