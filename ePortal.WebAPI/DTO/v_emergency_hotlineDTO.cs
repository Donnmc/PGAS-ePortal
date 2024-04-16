namespace ePortal.WebAPI.DTO
{
    public class v_emergency_hotlineDTO
    {
        public string Office { get; set; }

        public string Office_Abbreviation { get; set; }

        public List<v_emergency_hotlineLineDTO> v_emergency_hotlineLine { get; set; }
    }

    public class v_emergency_hotlineLineDTO
    {
        public string Line { get; set; }
        public List<v_emergency_hotlineAreaDTO> v_emergency_hotlineArea { get; set; }
    }

    public class v_emergency_hotlineAreaDTO
    {
        public string Area { get; set; }

        public string Hotline { get; set; }
    }
}
