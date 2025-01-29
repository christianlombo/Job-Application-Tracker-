using CsvHelper;
using JobApplicationTracker.Models;
using JobApplicationTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace JobApplicationTracker.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context) { _context = context; }

       public async Task<IActionResult> Index(string search, string status)
        {
            var userId = User.Identity.Name;
            var query = _context.JobApplications.Where(a => a.UserId == userId);

            if(!string.IsNullOrEmpty(search))
                query = query.Where(a => a.CompanyName.Contains(search) || a.JobTitle.Contains(search));

            if(!string.IsNullOrEmpty(status))
                query = query.Where(a => a.Status == status);

            var applications = await query.ToListAsync();
            return View(applications);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobApplication application)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    application.UserId = User.Identity?.Name;
                    application.CreatedAt = DateTime.Now;
                    _context.JobApplications.Add(application);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(application);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var application = await _context.JobApplications.FindAsync(id);
            if (application == null || application.UserId != User.Identity.Name)
                return Unauthorized();

            return View(application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobApplication application)
        {
            if (id != application.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var application = await _context.JobApplications.FindAsync(id);
            if (application == null || application.UserId != User.Identity.Name)
                return Unauthorized();

            return View(application);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);
            if (application != null && application.UserId == User.Identity.Name)
            {
                _context.JobApplications.Remove(application);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the job application by ID and ensure it belongs to the current user
            var application = await _context.JobApplications
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == User.Identity.Name);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }


        //public IActionResult Export()
        //{
        //    var userId = User.Identity.Name;
        //    var applications = _context.JobApplications.Where(a => a.UserId == userId).ToList();

        //    using (var memoryStream = new MemoryStream())
        //    using (var writer = new StreamWriter(memoryStream))
        //    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        //    {
        //        csv.WriteRecords(applications);
        //        writer.Flush();
        //        return File(memoryStream.ToArray(), "text/csv", "applications.csv");
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Import(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        using (var stream = file.OpenReadStream())
        //        using (var reader = new StreamReader(stream))
        //        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //        {
        //            var importedApplications = csv.GetRecords<JobApplication>().ToList();
        //            foreach (var app in importedApplications)
        //            {
        //                app.UserId = User.Identity.Name;
        //                _context.JobApplications.Add(app);
        //            }
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Dashboard()
        {
            var userId = User.Identity.Name;
            var applications = await _context.JobApplications.Where(a => a.UserId == userId).ToListAsync();

            var model = new
            {
                TotalApplications = applications.Count,
                Accepted = applications.Count(a => a.Status == "Accepted"),
                Rejected = applications.Count(a => a.Status == "Rejected"),
                Interviewing = applications.Count(a => a.Status == "Interviewing"),
                Applied = applications.Count(a => a.Status == "Applied")
            };

            return View(model);
        }
    }
}
