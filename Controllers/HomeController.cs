using JobApplicationTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobApplicationTracker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
