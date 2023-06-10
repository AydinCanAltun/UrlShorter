using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers {
    public class HomeController : Controller {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index() {
            
            return View();
        }

        [HttpGet]
        [Route("/{shortUrlId}")]
        public async Task<IActionResult> Index(string shortUrlId) {
            if (string.IsNullOrEmpty(shortUrlId)) {
                return View();
            }
            Url url = await _context.Url.Where(x => x.UrlId == shortUrlId).FirstOrDefaultAsync();
            if(url == null) {
                return View();
            }
            return Redirect(url.RedirectUrl);
        }

    }
}