namespace ePortal.WebAPI.DTO
{    public class BibleVerseDTO
    {
        public string Verse { get; set; } = string.Empty;
        public string Chapter { get; set; } = string.Empty;
        public DateOnly? Date { get; set; }
        public bool? Archive { get; set; }
    }

    public class BibleVerseLatestDTO
    {
        public string Verse { get; set; } = string.Empty;
        public string Chapter { get; set; } = string.Empty;
        public DateOnly? Date { get; set; }
    }
}
