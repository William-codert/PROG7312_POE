namespace PROG7312_POE.Models
{

    public class LocalEventsViewModel
    {
        public List<EventItem> Events { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<EventItem> RecentlyViewed { get; set; } = new();
        public List<EventItem> Recommendations { get; set; } = new();
        public List<AnnouncementItem> Announcements { get; set; } = new();
        public string? Keyword { get; set; }
        public string? SelectedCategory { get; set; }
        public string? SortOrder { get; set; }
        public DateTime? SelectedDate { get; set; }
    }

}
