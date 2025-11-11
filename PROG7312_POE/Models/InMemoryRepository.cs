using System.Collections;

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

        private readonly List<string> _searchedCategories;
        private readonly List<DateTime> _searchedDates;
        private readonly List<string> _searchedKeywords;
        private readonly List<ServiceRequest> _serviceRequests = new();


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

        public IEnumerable<EventItem> SearchEvents(string? keyword = null, string? category = null, DateTime? date = null)
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

            if (date.HasValue)
            {
                events = events.Where(e => e.Date.Date == date.Value.Date);
            }

            return events;
        }

        public IEnumerable<AnnouncementItem> SearchAnnouncements(string? keyword = null, DateTime? date = null)
        {
            IEnumerable<AnnouncementItem> announcements = _announcements;

            if (!string.IsNullOrEmpty(keyword))
            {
                announcements = announcements.Where(a =>
                    a.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    a.Body.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            if (date.HasValue)
            {
                announcements = announcements.Where(a => a.Posted.Date == date.Value.Date);
            }

            return announcements;
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

        public void TrackUserSearch(string? keyword, string? category, DateTime? date)
        {
            if (!string.IsNullOrEmpty(keyword))
                _searchedKeywords.Add(keyword);

            if (!string.IsNullOrEmpty(category))
                _searchedCategories.Add(category);

            if (date.HasValue)
                _searchedDates.Add(date.Value);

            if (_searchedKeywords.Count > 10) _searchedKeywords.RemoveAt(0);
            if (_searchedCategories.Count > 10) _searchedCategories.RemoveAt(0);
            if (_searchedDates.Count > 10) _searchedDates.RemoveAt(0);
        }

        public IEnumerable<EventItem> GetPersonalizedRecommendations()
        {
            if (!_searchedCategories.Any() && !_searchedDates.Any() && !_searchedKeywords.Any())
            {
                // 🧠 No searches yet — return nothing
                return Enumerable.Empty<EventItem>();
            }

            string? topCategory = _searchedCategories
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            DateTime? topDate = _searchedDates
                .GroupBy(d => d.Date)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var recommended = _sortedEvents.Values
                .Where(e =>
                    (topCategory != null && e.Category.Equals(topCategory, StringComparison.OrdinalIgnoreCase)) ||
                    (topDate.HasValue && Math.Abs((e.Date - topDate.Value).TotalDays) <= 3))
                .OrderBy(e => e.Date)
                .Take(3)
                .ToList();

            return recommended;
        }


        public void AddServiceRequest(ServiceRequest req)
        {
            _serviceRequests.Add(req);
        }

        public IEnumerable<ServiceRequest> GetServiceRequests()
        {
            return _serviceRequests.OrderBy(r => r.SubmittedAt);
        }

        public ServiceRequest? FindRequest(string refCode)
        {
            return _serviceRequests.FirstOrDefault(r => r.ReferenceCode.Equals(refCode, StringComparison.OrdinalIgnoreCase));
        }

        public void ClearSearchHistory()
        {
            _searchedKeywords.Clear();
            _searchedCategories.Clear();
            _searchedDates.Clear();
        }

        public void AddReport(ReportIssue report) => _reports.Add(report);
        public IEnumerable<ReportIssue> GetReports() => _reports;

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
        new() { Title = "Waste Management Awareness", Date = DateTime.UtcNow.AddDays(11), Location = "Library Conference Room", Summary = "Learn about recycling and composting.", Category = "Environment" },

        new() { Title = "Outdoor Yoga Session", Date = DateTime.UtcNow.AddDays(4), Location = "Sunrise Park", Summary = "Join a free outdoor yoga class for all skill levels.", Category = "Health" },
        new() { Title = "Tech Career Fair", Date = DateTime.UtcNow.AddDays(15), Location = "City Convention Centre", Summary = "Meet top tech companies and explore career opportunities.", Category = "Education" },
        new() { Title = "Charity Fun Run", Date = DateTime.UtcNow.AddDays(8), Location = "Riverside Track", Summary = "Participate in a 5km run to raise funds for local charities.", Category = "Community" },
        new() { Title = "Local Art Exhibition", Date = DateTime.UtcNow.AddDays(12), Location = "Art Gallery", Summary = "Explore works from talented local artists.", Category = "Entertainment" },
        new() { Title = "Farm-to-Table Cooking Workshop", Date = DateTime.UtcNow.AddDays(6), Location = "Community Kitchen", Summary = "Learn to cook healthy meals with fresh local produce.", Category = "Market" }
    };

            foreach (var e in sampleEvents)
                AddEvent(e);

            _announcements.AddRange(new List<AnnouncementItem>
    {
        new() { Title = "Water Disruption", Body = "Maintenance in northern suburbs 09:00–16:00.", Posted = DateTime.UtcNow.AddDays(-1) },
        new() { Title = "New Recycling Schedule", Body = "Collections move to Thursdays next month.", Posted = DateTime.UtcNow.AddDays(-3) },
        new() { Title = "Load Shedding Update", Body = "Stage 2 from 18:00 to 22:00 this week.", Posted = DateTime.UtcNow.AddDays(-2) },

        new() { Title = "Community Garden Opening", Body = "Join us this weekend to celebrate the opening of the new community garden.", Posted = DateTime.UtcNow.AddDays(-5) },
        new() { Title = "Library Extended Hours", Body = "Library will now be open until 20:00 on weekdays.", Posted = DateTime.UtcNow.AddDays(-4) },
        new() { Title = "City Marathon", Body = "The annual city marathon is scheduled for next Sunday.", Posted = DateTime.UtcNow.AddDays(-6) },
        new() { Title = "Local Theater Play", Body = "A new play opens at the community theater this Friday.", Posted = DateTime.UtcNow.AddDays(-2) },
        new() { Title = "Public Transport Update", Body = "Bus route 12 will be temporarily rerouted due to roadworks.", Posted = DateTime.UtcNow.AddDays(-3) }
    });

        SeedServiceRequests();
        }
        private void SeedServiceRequests()
        {
            var sampleRequests = new List<ServiceRequest>
        {
            new()
            {
                ReferenceCode = "SR1001",
                Category = "Waste Management",
                Location = "Central Square",
                Status = "Pending",
                Priority = 2,
                SubmittedAt = DateTime.UtcNow.AddDays(-3),
                Description = "Request to clear accumulated trash at Central Square."
            },
            new()
            {
                ReferenceCode = "SR1002",
                Category = "Road Maintenance",
                Location = "Maple Street",
                Status = "In Progress",
                Priority = 1,
                SubmittedAt = DateTime.UtcNow.AddDays(-1),
                Description = "Report a pothole on Maple Street near the school."
            },
            new()
            {
                ReferenceCode = "SR1003",
                Category = "Street Lighting",
                Location = "Oak Avenue",
                Status = "Completed",
                Priority = 3,
                SubmittedAt = DateTime.UtcNow.AddDays(-5),
                Description = "Street lights not functioning on Oak Avenue."
            },
            new()
            {
                ReferenceCode = "SR1004",
                Category = "Water Supply",
                Location = "Riverside Park",
                Status = "Pending",
                Priority = 2,
                SubmittedAt = DateTime.UtcNow.AddDays(-2),
                Description = "Water fountain at Riverside Park is leaking."
            },
            new()
            {
                ReferenceCode = "SR1005",
                Category = "Community Safety",
                Location = "Town Hall",
                Status = "In Progress",
                Priority = 1,
                SubmittedAt = DateTime.UtcNow.AddDays(-4),
                Description = "Request for increased patrol near Town Hall after hours."
            }
        };

            foreach (var req in sampleRequests)
            {
                AddServiceRequest(req);
            }
        }

    }
}
