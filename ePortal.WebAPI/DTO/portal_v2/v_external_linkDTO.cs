namespace PGAS.WebAPI.DTO.ePortal
{
    public class v_external_linkParentListDTO
    {
        public string? Parent_List_Name { get; set; }
        public string? Parent_List_Icon { get; set; }
        public string? Parent_List_Link { get; set; }
        public DateTime? Parent_List_Date_Created { get; set; }
        public int? Parent_List_Order { get; set; }
        public bool IsEditing { get; set; }
        public bool ShowDetails { get; set; }
        public List<v_external_linkChildListDTO>? v_external_linkChildList { get; set; }
    }
    public class v_external_linkChildListDTO
    {
        public string? Child_List_Name { get; set; }
        public string? Child_List_Icon { get; set; }
        public string? Child_List_Link { get; set; }
        public DateTime? Child_List_Date_Created { get; set; }
        public int? Child_List_Order { get; set; }
        public bool IsEditing { get; set; }
    }
}
