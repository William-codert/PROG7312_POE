using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;

namespace PROG7312_POE.Controllers
{
    public class ReportIssuesController : Controller
    {
        private readonly InMemoryRepository _repo;
        private readonly IWebHostEnvironment _env;

        public ReportIssuesController(InMemoryRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(20_000_000)] 
        public async Task<IActionResult> Create(ReportIssue model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.AttachmentFile != null && model.AttachmentFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Path.GetRandomFileName() + Path.GetExtension(model.AttachmentFile.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AttachmentFile.CopyToAsync(stream);
                }
                model.AttachmentPath = "/uploads/" + Path.GetFileName(filePath);
            }

            model.ReportedAt = DateTime.UtcNow;
            model.Id = Guid.NewGuid();
            _repo.AddReport(model);

            TempData["Success"] = "Thank you — your issue has been submitted. We will review it and update the status.";
            return RedirectToAction("Create");
        }
    }
}