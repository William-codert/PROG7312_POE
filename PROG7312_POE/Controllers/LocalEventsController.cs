using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;
using System;
using System.Linq;

namespace PROG7312_POE.Controllers
{
    public class LocalEventsController : Controller
    {
        private readonly InMemoryRepository _repo;

        public LocalEventsController(InMemoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index(string? keyword, string? category, DateTime? eventDate, string? sortOrder)
        {
            // Log user search pattern for recommendation tracking
            _repo.TrackUserSearch(keyword, category, eventDate);

            var events = _repo.SearchEvents(keyword, category);

            // ✅ Filter by date if provided
            if (eventDate.HasValue)
            {
                events = events.Where(e => e.Date.Date == eventDate.Value.Date);
            }

            // ✅ Sorting logic
            events = sortOrder switch
            {
                "date_desc" => events.OrderByDescending(e => e.Date),
                "name" => events.OrderBy(e => e.Title),
                "name_desc" => events.OrderByDescending(e => e.Title),
                "category" => events.OrderBy(e => e.Category),
                "category_desc" => events.OrderByDescending(e => e.Category),
                _ => events.OrderBy(e => e.Date)
            };

            var vm = new LocalEventsViewModel
            {
                Events = events.ToList(),
                Categories = _repo.GetCategories().ToList(),
                Recommendations = _repo.GetPersonalizedRecommendations().ToList(),
                RecentlyViewed = _repo.GetRecentlyViewed().ToList(),
                Keyword = keyword,
                SelectedCategory = category,
                SortOrder = sortOrder,
                SelectedDate = eventDate
            };

            return View(vm);
        }
    }

    public class LocalEventsViewModel
    {
        public List<EventItem> Events { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<EventItem> RecentlyViewed { get; set; } = new();
        public List<EventItem> Recommendations { get; set; } = new();
        public string? Keyword { get; set; }
        public string? SelectedCategory { get; set; }
        public string? SortOrder { get; set; }
        public DateTime? SelectedDate { get; set; }
    }
}
