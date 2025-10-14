using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;

namespace PROG7312_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly InMemoryRepository _repo;


        public HomeController(InMemoryRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var vm = new LandingViewModel
            {
                MunicipalityName = "SiguduVille Municipality",
                Tagline = "Connecting Citizens to Services",
                FeaturedEvents = _repo.GetSortedEvents().ToList(),         
                Announcements = _repo.GetAnnouncements().ToList()     
            };

            return View(vm);
        }
    }

    public class LandingViewModel
    {
        public string MunicipalityName { get; set; } = "";
        public string Tagline { get; set; } = "";
        public List<EventItem> FeaturedEvents { get; set; } = new();
        public List<AnnouncementItem> Announcements { get; set; } = new();

    }
}
