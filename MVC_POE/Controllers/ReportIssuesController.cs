using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MVC_POE.Models;
using MVC_POE.Services;
using System.Reflection;

namespace MVC_POE.Controllers
{
    public class ReportIssuesController : Controller
    {
        private readonly HashSetService _hashSetService;
        private readonly IWebHostEnvironment _webHost;
        public ReportIssuesController(HashSetService hashSetService, IWebHostEnvironment webHost)
        {
            _hashSetService = hashSetService;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateReportIssues()
        {
            return View(new ReportIssuesForm());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReportIssues(ReportIssuesForm model)
        {
            // Just return the form pre-filled with the data
            return View(model);
        }

        // Displays the form for citizens to submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowReportIssuesConfirmation(ReportIssuesForm reportIssuesForm, IFormFile? file)
        {
            if (reportIssuesForm.FormId == Guid.Empty)
            {
                reportIssuesForm.FormId = Guid.NewGuid();
            }

            if (file != null && file.Length > 0)
            {
                string uploadFolder = Path.Combine(_webHost.WebRootPath, "uploads");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string fileName = Path.GetFileName(file.FileName);
                string fileSavePath = Path.Combine(uploadFolder, fileName);

                using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Store relative path for preview
                reportIssuesForm.MediaAttachment = "/uploads/" + fileName;
            }

            if (ModelState.IsValid)
            {
                // Just pass the model to the confirmation page
                return View("ReportIssuesConfirmation", reportIssuesForm);
            }

            return View(reportIssuesForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmReportIssues(ReportIssuesForm reportIssuesForm)
        {
            if (ModelState.IsValid)
            {
                _hashSetService.AddForm(reportIssuesForm);
                return RedirectToAction("ViewReportIssues"); // shows list
            }

            return View("ReportIssuesConfirmation", reportIssuesForm);
        }

        // Displays all submissions
        public IActionResult ViewReportIssues(string location)
        {
            var forms = _hashSetService.GetAllForms();

            if (!string.IsNullOrEmpty(location))
            {
                forms = forms.Where(f => f.Location.Contains(location, StringComparison.OrdinalIgnoreCase));
            }

            ViewData["CurrentLocation"] = location;

            return View(forms);
        }


    }
}
