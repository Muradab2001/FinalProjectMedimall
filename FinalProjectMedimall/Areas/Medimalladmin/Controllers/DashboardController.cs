using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{
    [Area("Medimalladmin")]
    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DashboardController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Category> model = _context.Categories.ToList();
            return View(model);
        }
    }
}
