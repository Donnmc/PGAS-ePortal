namespace PGAS.WebAPI.DTO.ePortal
{
    public class v_clustered_information_systemDTO
    {
        public string? Cluster_Name { get; set; }
        public string? Cluster_Abbreviation { get; set; }
        public string? Cluster_Icon { get; set; }
        public bool? Cluster___Is_Active_ { get; set; }
        public List<InformationSystemDTO>? Information_Systems { get; set; }
    }

    public class InformationSystemDTO
    {
        public string? Information_System_Name { get; set; }
        public string? Information_System_Abbreviation { get; set; }
        public string? Information_System_Icon { get; set; }
        public string? Information_System_Link { get; set; }
        public string? Information_System_Platform { get; set; }
        public bool? Information_System___Is_Active_ { get; set; }
    }
}
