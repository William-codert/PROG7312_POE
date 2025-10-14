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
        public IActionResult Index(string? keyword, string? category, string? sortOrder)
        {
            var events = _repo.SearchEvents(keyword, category);

            if (sortOrder == "date_desc")
                events = events.OrderByDescending(e => e.Date);
            else
                events = events.OrderBy(e => e.Date);

            var vm = new LocalEventsViewModel
            {
                Events = events.ToList(),
                Categories = _repo.GetCategories().ToList(),
                Recommendations = _repo.GetRecommendations().ToList(),
                Keyword = keyword,
                SelectedCategory = category,
                SortOrder = sortOrder
            };
            return View(vm);
        }
    }

    public class LocalEventsViewModel
    {
        public List<EventItem> Events { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
        public string? Keyword { get; set; }
        public string? SelectedCategory { get; set; }
        public string? SortOrder { get; set; }
    }
}
