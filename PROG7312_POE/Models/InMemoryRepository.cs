using Microsoft.Extensions.Logging;
using System;
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
        private Queue<string> _recentSearches = new Queue<string>();
        private readonly List<ReportIssue> _reports = new List<ReportIssue>();
        private readonly List<AnnouncementItem> _announcements = new List<AnnouncementItem>();

        public InMemoryRepository()
        {
            Seed();
        }

        // ------------------ Events / Search ------------------
        public IEnumerable<EventItem> SearchEvents(string? keyword = null, string? category = null)
        {
            IEnumerable<EventItem> events = _sortedEvents.Values;

            if (!string.IsNullOrEmpty(keyword))
            {
                _recentSearches.Enqueue(keyword);
                if (_recentSearches.Count > 5) _recentSearches.Dequeue();
                events = events.Where(e => e.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                                         || e.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category))
            {
                events = events.Where(e => e.Location.Contains(category, StringComparison.OrdinalIgnoreCase));
            }

            return events;
        }

        public IEnumerable<string> GetRecommendations()
        {
            var lastKeyword = _recentSearches.LastOrDefault();
            if (string.IsNullOrEmpty(lastKeyword))
                return new List<string>();

            return _sortedEvents.Values
                .Where(e => e.Title.Contains(lastKeyword, StringComparison.OrdinalIgnoreCase))
                .Select(e => e.Title)
                .Take(3);
        }

        public IEnumerable<EventItem> GetSortedEvents() => _sortedEvents.Values;
        public IEnumerable<string> GetCategories() => _categories;

        // Optional backward compatibility
        public IEnumerable<EventItem> GetEvents() => GetSortedEvents();

        // ------------------ Reports ------------------
        public void AddReport(ReportIssue report)
        {
            _reports.Add(report);
            Console.WriteLine($"Report Added: {report.Category} - {report.Description}");
        }

        public IEnumerable<ReportIssue> GetReports() => _reports;

        // ------------------ Announcements ------------------
        public IEnumerable<AnnouncementItem> GetAnnouncements() => _announcements;

        // ------------------ Seed Data ------------------
        private void Seed()
        {
            _sortedEvents = new SortedDictionary<DateTime, EventItem>();
            _categories = new HashSet<string>();

            // ======== Events ========
            var sampleEvents = new List<EventItem>
    {
        new() { Title = "Community Clean-up Day", Date = DateTime.UtcNow.AddDays(7), Location = "Riverside Park", Summary = "Join your neighbours for a morning of cleaning and recycling." },
        new() { Title = "Youth Skills Workshop", Date = DateTime.UtcNow.AddDays(14), Location = "Town Hall", Summary = "Free workshops on basic computer skills and CV writing." },
        new() { Title = "Local Farmers Market", Date = DateTime.UtcNow.AddDays(5), Location = "Central Square", Summary = "Support local farmers and artisans every Saturday." },
        new() { Title = "Music in the Park", Date = DateTime.UtcNow.AddDays(10), Location = "Riverbank Amphitheatre", Summary = "An evening of local jazz and family fun." },
        new() { Title = "Blood Donation Drive", Date = DateTime.UtcNow.AddDays(3), Location = "Civic Centre", Summary = "Help save lives by donating blood today." },
        new() { Title = "Town Hall Budget Meeting", Date = DateTime.UtcNow.AddDays(20), Location = "Town Hall", Summary = "Public meeting to discuss next year’s municipal budget." },
        new() { Title = "Pet Adoption Fair", Date = DateTime.UtcNow.AddDays(2), Location = "Animal Shelter Grounds", Summary = "Find your new furry friend at our weekend fair." },
        new() { Title = "Waste Management Awareness", Date = DateTime.UtcNow.AddDays(11), Location = "Library Conference Room", Summary = "Learn more about responsible recycling and composting." },
        new() { Title = "Youth Soccer Tournament", Date = DateTime.UtcNow.AddDays(6), Location = "Northfield Stadium", Summary = "Annual 5-a-side youth tournament – all welcome!" },
        new() { Title = "Senior Citizens Tea Party", Date = DateTime.UtcNow.AddDays(4), Location = "Community Centre", Summary = "An afternoon of games and light refreshments." },
        new() { Title = "Art & Culture Exhibition", Date = DateTime.UtcNow.AddDays(8), Location = "City Gallery", Summary = "Showcasing the creative talent of SiguduVille artists." },
        new() { Title = "STEM Fair for Students", Date = DateTime.UtcNow.AddDays(15), Location = "SiguduVille High School", Summary = "Hands-on learning and science fun for all ages." },
        new() { Title = "Green Market Day", Date = DateTime.UtcNow.AddDays(9), Location = "Eco Park", Summary = "Buy organic produce directly from local farms." },
        new() { Title = "Volunteer Firefighter Drive", Date = DateTime.UtcNow.AddDays(18), Location = "Fire Department HQ", Summary = "Join our dedicated volunteer response team." },
        new() { Title = "Community Talent Show", Date = DateTime.UtcNow.AddDays(12), Location = "Central Hall", Summary = "Bring your family and enjoy local entertainment." }
    };

            foreach (var e in sampleEvents)
            {
                _sortedEvents[e.Date] = e;
                _categories.Add(e.Location);
            }

            // ======== Announcements ========
            _announcements.AddRange(new List<AnnouncementItem>
    {
        new() { Title = "Water Disruption", Body = "Planned maintenance in the northern suburbs between 09:00 and 16:00.", Posted = DateTime.UtcNow.AddDays(-1) },
        new() { Title = "New Recycling Schedule", Body = "Weekly recycling collections move to Thursdays starting next month.", Posted = DateTime.UtcNow.AddDays(-3) },
        new() { Title = "Load Shedding Update", Body = "Stage 2 expected from 18:00 to 22:00 this week.", Posted = DateTime.UtcNow.AddDays(-2) },
        new() { Title = "Park Renovation Completed", Body = "Riverside Park is open again after upgrades.", Posted = DateTime.UtcNow.AddDays(-5) },
        new() { Title = "Traffic Alert", Body = "Expect road closures along Main Street due to maintenance.", Posted = DateTime.UtcNow.AddDays(-4) },
        new() { Title = "Library Holiday Hours", Body = "The municipal library will close at 14:00 during the holidays.", Posted = DateTime.UtcNow.AddDays(-10) },
        new() { Title = "Free Internet Access", Body = "Public Wi-Fi now available at all municipal offices.", Posted = DateTime.UtcNow.AddDays(-7) },
        new() { Title = "Water Conservation Tips", Body = "Simple steps to reduce daily water use.", Posted = DateTime.UtcNow.AddDays(-6) },
        new() { Title = "Health Fair Registration", Body = "Register for the annual community health screening event.", Posted = DateTime.UtcNow.AddDays(-8) },
        new() { Title = "Public Transport Expansion", Body = "New routes added to the bus schedule.", Posted = DateTime.UtcNow.AddDays(-9) }
    });

            // ======== Reports ========
            for (int i = 1; i <= 5; i++)
            {
                _reports.Add(new ReportIssue
                {
                    Id = Guid.NewGuid(),
                    Location = $"{i} Elm Street, Ward {i}",
                    Category = i % 2 == 0 ? "Roads" : "Water",
                    Description = i % 2 == 0
                        ? "Large pothole forming near school entrance."
                        : "Intermittent water supply in residential area.",
                    ReportedAt = DateTime.UtcNow.AddDays(-i),
                    Status = i % 2 == 0 ? "Under Review" : "Resolved"
                });
            }
        }

    }
}
