using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;

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
            // 🧹 If no filters are applied (fresh page load), clear any previous search history
            if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(category) && !eventDate.HasValue)
            {
                _repo.ClearSearchHistory();
            }

            // 🔍 Perform the event and announcement searches
            var events = _repo.SearchEvents(keyword, category, eventDate);
            var announcements = _repo.SearchAnnouncements(keyword, eventDate);

            // 🧠 Only track user search if there were any matching events
            if (events.Any())
            {
                _repo.TrackUserSearch(keyword, category, eventDate);
            }

            // ⬆ Sorting logic
            events = sortOrder switch
            {
                "date_desc" => events.OrderByDescending(e => e.Date),
                "name" => events.OrderBy(e => e.Title),
                "name_desc" => events.OrderByDescending(e => e.Title),
                "category" => events.OrderBy(e => e.Category),
                "category_desc" => events.OrderByDescending(e => e.Category),
                _ => events.OrderBy(e => e.Date)
            };

            // 💡 Get personalized recommendations *after* tracking searches
            var recommendations = _repo.GetPersonalizedRecommendations().ToList();

            // 📦 Build ViewModel
            var vm = new LocalEventsViewModel
            {
                Events = events.ToList(),
                Categories = _repo.GetCategories().ToList(),
                Recommendations = recommendations,
                RecentlyViewed = _repo.GetRecentlyViewed().ToList(),
                Announcements = announcements.ToList(),
                Keyword = keyword,
                SelectedCategory = category,
                SortOrder = sortOrder,
                SelectedDate = eventDate
            };

            return View(vm);
        }
    }
}
