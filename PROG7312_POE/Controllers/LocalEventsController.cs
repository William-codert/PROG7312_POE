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
            // Clear old searches when user comes to this page fresh
            if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(category) && !eventDate.HasValue)
            {
                _repo.ClearSearchHistory();
            }

            _repo.TrackUserSearch(keyword, category, eventDate);

            var events = _repo.SearchEvents(keyword, category, eventDate);

            events = sortOrder switch
            {
                "date_desc" => events.OrderByDescending(e => e.Date),
                "name" => events.OrderBy(e => e.Title),
                "name_desc" => events.OrderByDescending(e => e.Title),
                "category" => events.OrderBy(e => e.Category),
                "category_desc" => events.OrderByDescending(e => e.Category),
                _ => events.OrderBy(e => e.Date)
            };

            var announcements = _repo.SearchAnnouncements(keyword, eventDate);

            var vm = new LocalEventsViewModel
            {
                Events = events.ToList(),
                Categories = _repo.GetCategories().ToList(),
                Recommendations = _repo.GetPersonalizedRecommendations().ToList(),
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
