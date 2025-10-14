using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PROG7312_POE.Models
{
    public class EventItem
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }

    public class AnnouncementItem
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Posted { get; set; }
    }

    public class InMemoryRepository
    {
        private SortedDictionary<DateTime, EventItem> _sortedEvents;
        private HashSet<string> _categories;
        private readonly Hashtable _eventTable;            // ✅ Hashtable for fast lookup
        private readonly Queue<EventItem> _recentlyViewed;  // ✅ Queue for "last viewed" or recently added
        private readonly Queue<string> _recentSearches;     // Search history (existing)
        private readonly List<ReportIssue> _reports;
        private readonly List<AnnouncementItem> _announcements;

        public InMemoryRepository()
        {
            _sortedEvents = new();
            _categories = new();
            _eventTable = new Hashtable();
            _recentlyViewed = new Queue<EventItem>();
            _recentSearches = new Queue<string>();
            _reports = new List<ReportIssue>();
            _announcements = new List<AnnouncementItem>();

            Seed();
        }

        // -------------------- EVENTS --------------------

        public IEnumerable<EventItem> SearchEvents(string? keyword = null, string? category = null)
        {
            IEnumerable<EventItem> events = _sortedEvents.Values;

            if (!string.IsNullOrEmpty(keyword))
            {
                _recentSearches.Enqueue(keyword);
                if (_recentSearches.Count > 5)
                    _recentSearches.Dequeue();

                events = events.Where(e =>
                    e.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    e.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    e.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category))
            {
                events = events.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return events;
        }

        public IEnumerable<EventItem> GetSortedEvents() => _sortedEvents.Values;
        public IEnumerable<string> GetCategories() => _categories;
        public IEnumerable<AnnouncementItem> GetAnnouncements() => _announcements;

        // Add new event
        public void AddEvent(EventItem e)
        {
            _sortedEvents[e.Date] = e;
            _categories.Add(e.Category);
            _eventTable[e.Title] = e; // store by title
            _recentlyViewed.Enqueue(e);

            // Keep queue manageable
            if (_recentlyViewed.Count > 10)
                _recentlyViewed.Dequeue();
        }

        // Quick lookup from Hashtable
        public EventItem? FindEventByTitle(string title)
        {
            return _eventTable.ContainsKey(title)
                ? (EventItem)_eventTable[title]!
                : null;
        }

        public IEnumerable<EventItem> GetRecentlyViewed() => _recentlyViewed.ToList();

        // Recommendations based on last searched term
        public IEnumerable<string> GetRecommendations()
        {
            var lastKeyword = _recentSearches.LastOrDefault();
            if (string.IsNullOrEmpty(lastKeyword))
                return Enumerable.Empty<string>();

            return _sortedEvents.Values
                .Where(e => e.Title.Contains(lastKeyword, StringComparison.OrdinalIgnoreCase))
                .Select(e => e.Title)
                .Take(3);
        }

        // -------------------- REPORTS --------------------
        public void AddReport(ReportIssue report) => _reports.Add(report);
        public IEnumerable<ReportIssue> GetReports() => _reports;

        // -------------------- SEED DATA --------------------
        private void Seed()
        {
            var sampleEvents = new List<EventItem>
            {
                new() { Title = "Community Clean-up Day", Date = DateTime.UtcNow.AddDays(7), Location = "Riverside Park", Summary = "Join your neighbours for a morning of cleaning and recycling.", Category = "Community" },
                new() { Title = "Youth Skills Workshop", Date = DateTime.UtcNow.AddDays(14), Location = "Town Hall", Summary = "Free workshops on basic computer skills and CV writing.", Category = "Education" },
                new() { Title = "Local Farmers Market", Date = DateTime.UtcNow.AddDays(5), Location = "Central Square", Summary = "Support local farmers and artisans every Saturday.", Category = "Market" },
                new() { Title = "Music in the Park", Date = DateTime.UtcNow.AddDays(10), Location = "Riverbank Amphitheatre", Summary = "An evening of local jazz and family fun.", Category = "Entertainment" },
                new() { Title = "Blood Donation Drive", Date = DateTime.UtcNow.AddDays(3), Location = "Civic Centre", Summary = "Help save lives by donating blood today.", Category = "Health" },
                new() { Title = "Town Hall Budget Meeting", Date = DateTime.UtcNow.AddDays(20), Location = "Town Hall", Summary = "Public meeting to discuss next year’s municipal budget.", Category = "Government" },
                new() { Title = "Pet Adoption Fair", Date = DateTime.UtcNow.AddDays(2), Location = "Animal Shelter Grounds", Summary = "Find your new furry friend at our weekend fair.", Category = "Community" },
                new() { Title = "Waste Management Awareness", Date = DateTime.UtcNow.AddDays(11), Location = "Library Conference Room", Summary = "Learn about responsible recycling and composting.", Category = "Environment" }
            };

            foreach (var e in sampleEvents)
                AddEvent(e);

            _announcements.AddRange(new List<AnnouncementItem>
            {
                new() { Title = "Water Disruption", Body = "Planned maintenance in the northern suburbs between 09:00 and 16:00.", Posted = DateTime.UtcNow.AddDays(-1) },
                new() { Title = "New Recycling Schedule", Body = "Weekly recycling collections move to Thursdays starting next month.", Posted = DateTime.UtcNow.AddDays(-3) },
                new() { Title = "Load Shedding Update", Body = "Stage 2 expected from 18:00 to 22:00 this week.", Posted = DateTime.UtcNow.AddDays(-2) }
            });
        }
    }
}
