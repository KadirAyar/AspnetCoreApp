using FormDeneme.Areas.Identity.Data;
using FormDeneme.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FormDeneme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize]
        public IActionResult Index(string search)
        {
            ViewData["Search"] = search;
            var form = from b in _context.Form
                       select b;
            List<Form> forms = new List<Form>();
            foreach(Form f in form)
            {
                forms.Add(f);
            }
            if (!String.IsNullOrEmpty(search))
            {
                form = form.Where(x => x.Name.Contains(search));
            }
            return View(form);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}