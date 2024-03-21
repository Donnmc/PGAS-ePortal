namespace ePortal.WebAPI.DTO
{    public class BibleVerseDTO
    {
        public string Verse { get; set; }
        public string Chapter { get; set; }
        public DateOnly? Date { get; set; }
        public bool? Archive { get; set; }
    }

    public class BibleVerseLatestDTO
    {
        public string Verse { get; set; }
        public string Chapter { get; set; }
        public DateOnly? Date { get; set; }
    }
}
