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
        private readonly Hashtable _eventTable;
        private readonly Queue<EventItem> _recentlyViewed;
        private readonly List<AnnouncementItem> _announcements;
        private readonly List<ReportIssue> _reports;

        // ✅ New: Track user search patterns
        private readonly List<string> _searchedCategories;
        private readonly List<DateTime> _searchedDates;
        private readonly List<string> _searchedKeywords;

        public InMemoryRepository()
        {
            _sortedEvents = new();
            _categories = new();
            _eventTable = new Hashtable();
            _recentlyViewed = new Queue<EventItem>();
            _announcements = new List<AnnouncementItem>();
            _reports = new List<ReportIssue>();

            _searchedCategories = new List<string>();
            _searchedDates = new List<DateTime>();
            _searchedKeywords = new List<string>();

            Seed();
        }

        // -------------------- EVENTS --------------------
        public IEnumerable<EventItem> SearchEvents(string? keyword = null, string? category = null)
        {
            IEnumerable<EventItem> events = _sortedEvents.Values;

            if (!string.IsNullOrEmpty(keyword))
            {
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

        public void AddEvent(EventItem e)
        {
            _sortedEvents[e.Date] = e;
            _categories.Add(e.Category);
            _eventTable[e.Title] = e;
            _recentlyViewed.Enqueue(e);

            if (_recentlyViewed.Count > 10)
                _recentlyViewed.Dequeue();
        }

        public EventItem? FindEventByTitle(string title)
        {
            return _eventTable.ContainsKey(title)
                ? (EventItem)_eventTable[title]!
                : null;
        }

        public IEnumerable<EventItem> GetRecentlyViewed() => _recentlyViewed.ToList();
        public IEnumerable<string> GetCategories() => _categories;
        public IEnumerable<AnnouncementItem> GetAnnouncements() => _announcements;

        // -------------------- USER SEARCH TRACKING --------------------
        public void TrackUserSearch(string? keyword, string? category, DateTime? date)
        {
            if (!string.IsNullOrEmpty(keyword))
                _searchedKeywords.Add(keyword);

            if (!string.IsNullOrEmpty(category))
                _searchedCategories.Add(category);

            if (date.HasValue)
                _searchedDates.Add(date.Value);

            // Keep only recent 10 searches
            if (_searchedKeywords.Count > 10) _searchedKeywords.RemoveAt(0);
            if (_searchedCategories.Count > 10) _searchedCategories.RemoveAt(0);
            if (_searchedDates.Count > 10) _searchedDates.RemoveAt(0);
        }

        // ✅ Recommendation algorithm: Based on most searched category/date
        public IEnumerable<EventItem> GetPersonalizedRecommendations()
        {
            if (!_searchedCategories.Any() && !_searchedDates.Any())
                return Enumerable.Empty<EventItem>();

            // Find the most frequently searched category
            string? topCategory = _searchedCategories
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            // Find the most searched date range
            DateTime? topDate = _searchedDates
                .GroupBy(d => d.Date)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            // Recommend events that match top category or similar date
            var recommended = _sortedEvents.Values
                .Where(e =>
                    (topCategory != null && e.Category.Equals(topCategory, StringComparison.OrdinalIgnoreCase)) ||
                    (topDate.HasValue && Math.Abs((e.Date - topDate.Value).TotalDays) <= 2))
                .OrderBy(e => e.Date)
                .Take(3)
                .ToList();

            return recommended;
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
                new() { Title = "Youth Skills Workshop", Date = DateTime.UtcNow.AddDays(14), Location = "Town Hall", Summary = "Free workshops on computer skills and CV writing.", Category = "Education" },
                new() { Title = "Local Farmers Market", Date = DateTime.UtcNow.AddDays(5), Location = "Central Square", Summary = "Support local farmers every Saturday.", Category = "Market" },
                new() { Title = "Music in the Park", Date = DateTime.UtcNow.AddDays(10), Location = "Riverbank Amphitheatre", Summary = "Enjoy local jazz and family fun.", Category = "Entertainment" },
                new() { Title = "Blood Donation Drive", Date = DateTime.UtcNow.AddDays(3), Location = "Civic Centre", Summary = "Donate blood and save lives.", Category = "Health" },
                new() { Title = "Town Hall Budget Meeting", Date = DateTime.UtcNow.AddDays(20), Location = "Town Hall", Summary = "Discuss next year’s municipal budget.", Category = "Government" },
                new() { Title = "Pet Adoption Fair", Date = DateTime.UtcNow.AddDays(2), Location = "Animal Shelter Grounds", Summary = "Find your new furry friend.", Category = "Community" },
                new() { Title = "Waste Management Awareness", Date = DateTime.UtcNow.AddDays(11), Location = "Library Conference Room", Summary = "Learn about recycling and composting.", Category = "Environment" }
            };

            foreach (var e in sampleEvents)
                AddEvent(e);

            _announcements.AddRange(new List<AnnouncementItem>
            {
                new() { Title = "Water Disruption", Body = "Maintenance in northern suburbs 09:00–16:00.", Posted = DateTime.UtcNow.AddDays(-1) },
                new() { Title = "New Recycling Schedule", Body = "Collections move to Thursdays next month.", Posted = DateTime.UtcNow.AddDays(-3) },
                new() { Title = "Load Shedding Update", Body = "Stage 2 from 18:00 to 22:00 this week.", Posted = DateTime.UtcNow.AddDays(-2) }
            });
        }
    }
}
