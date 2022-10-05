using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FinalProjectMedimall.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;
   
        public AboutController(ApplicationDbContext context)
        {
            _context = context;
    
        }
        public IActionResult Index()
        {
            About about = _context.Abouts.FirstOrDefault();  
            return View(about);
        }
    }
}
