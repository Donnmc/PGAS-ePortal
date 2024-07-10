namespace PGAS.WebAPI.DTO.ePortal
{
    public class v_emergency_hotlineOfficeDTO
    {
        public string? Office { get; set; }

        public string? Office_Abbreviation { get; set; }

        public List<v_emergency_hotlineLineDTO>? v_emergency_hotlineLine { get; set; }
    }

    public class v_emergency_hotlineLineDTO
    {
        public string? Line { get; set; }
        public List<v_emergency_hotlineDTO>? v_emergency_hotlineArea { get; set; }
    }

    public class v_emergency_hotlineDTO
    {
        public string? Area { get; set; }

        public string? Hotline { get; set; }
    }
}
