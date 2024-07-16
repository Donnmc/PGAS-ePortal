namespace PGAS.WebAPI.Entities.portal_v2.Table
{
    public partial class vw_external_link
    {
        public string? Parent_List_Name { get; set; }
        public string? Parent_List_Icon { get; set; }
        public string? Parent_List_Link { get; set; }
        public int? Parent_List_Order { get; set; }
        public DateTime? Parent_List_Date_Created { get; set; }
        public string? Child_List_Name { get; set; }
        public string? Child_List_Link { get; set; }
        public string? Child_List_Icon { get; set; }
        public int? Child_List_Order { get; set; }
        public DateTime? Child_List_Date_Created { get; set; }
    }
}