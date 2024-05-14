namespace ePortal.WebAPI.DTO.ePortal
{
    public class v_ip_phone_directoryLineDTO
    {
        public string Line { get; set; }

        public List<v_ip_phone_directoryAreaDTO> v_ip_phone_directoryArea { get; set; }
    }

    public class v_ip_phone_directoryAreaDTO
    {
        public string Area { get; set; }
        public List<v_ip_phone_directoryOfficeDTO> v_ip_phone_directoryOffice { get; set; }

    }

    public class v_ip_phone_directoryOfficeDTO
    {
        public string Office { get; set; }

        public string Office_Abbreviation { get; set; }
        public List<v_ip_phone_directoryDTO> v_ip_phone_directoryOfficeArea { get; set; }
    }

    public class v_ip_phone_directoryDTO
    {
        public string Office_Area { get; set; }
        public string Line_Number { get; set; }
    }
}
