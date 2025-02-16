using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

    }
}
