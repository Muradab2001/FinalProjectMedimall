using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FinalProjectMedimall.Utilities;
using PagedList;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{
    [Area("Medimalladmin")]
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<About> abouts = _context.Abouts.ToList();
            _context.SaveChanges();
            return View(abouts);
        }
    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            About about = await _context.Abouts.FirstOrDefaultAsync(s => s.Id == id);
            if (about == null) return NotFound();
            return View(about);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, About about)
        {
            if (id == null || id == 0) return NotFound();
            About existed = await _context.Abouts.FindAsync(id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid) return View(about);
            if (about.Photo == null)
            {
                string filename = existed.Image;
                _context.Entry(existed).CurrentValues.SetValues(about);
                existed.Image = filename;
            }
            else
            {
                if (!about.Photo.ImageIsOkay(3))
                {
                    ModelState.AddModelError("Photo", "choose image file");
                    return View(existed);
                }
                FileValidator.FileDelete(_env.WebRootPath, "assets/image", existed.Image);
                _context.Entry(existed).CurrentValues.SetValues(about);
                existed.Image = await about.Photo.FileCreate(_env.WebRootPath, "assets/image");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
