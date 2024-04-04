namespace ePortal.WebAPI.DTO
{
    public class v_clustered_information_systemDTO
    {
        public string Cluster_Name { get; set; } = string.Empty;
        public string Cluster_Abbreviation { get; set; } = string.Empty;
        public string Cluster_Icon { get; set; } = string.Empty;
        public bool? Cluster___Is_Active_ { get; set; }
        public List<InformationSystemDTO> Information_Systems { get; set; } // List of information systems
    }

    public class InformationSystemDTO
    {
        public string Information_System_Name { get; set; } = string.Empty;
        public string Information_System_Abbreviation { get; set; } = string.Empty;
        public string Information_System_Icon { get; set; } = string.Empty;
        public string Information_System_Link { get; set; } = string.Empty;
        public string Information_System_Platform { get; set; } = string.Empty;
        public bool? Information_System___Is_Active_ { get; set; }
    }
}
