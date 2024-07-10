namespace PGAS.WebAPI.Entities.db_pmis.pmis.View
{
    public class vw_pgas_employees
    {
        public long eid { get; set; }
        public string? SwipeID { get; set; }
        public string? OfficeName { get; set; }
        public string? OfficeAbbr { get; set; }
        public string? EmployeeName { get; set; }
        public string? Position { get; set; }
        public byte? SG { get; set; }
        public string? Status { get; set; }
        public bool? isactive { get; set; }
        public string? Telephone { get; set; }
        public string? EmailAdd { get; set; }
        public string? Cause { get; set; }
        public DateTime? AppointCoverage { get; set; }
    }
}
