using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using FinalProjectMedimall.Utilities;
using FinalProjectMedimall.DAL;
using System.Linq; 
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{

        [Area("Medimalladmin")]
        public class SliderController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly IWebHostEnvironment _env;

            public SliderController(ApplicationDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }
            public IActionResult Index()
            {
                List<Slider> model = _context.Sliders.ToList();
                return View(model);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [AutoValidateAntiforgeryToken]
            public async Task<IActionResult> Create(Slider slider)
            {
                if (slider.Photo is null)
                {
                    ModelState.AddModelError("Photo", "plase choose image file");
                }
                int count = 0;
                foreach (var item in _context.Sliders)
                {
                    if (item.Id != null)
                        count++;
                }
                if (count == 3)
                {
                    ModelState.AddModelError("Order", "You have exceeded the image limit");
                    return View();
                }
                if (!ModelState.IsValid) return View();
                if (!slider.Photo.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Photo", "plase choose image file");
                    return View();
                }
                slider.Image = await slider.Photo.FileCreate(_env.WebRootPath, "assets/image/");
                await _context.Sliders.AddAsync(slider);
                _context.SaveChanges();

                if (slider.Photo.Length / 1024 / 1024 > 2)
                {
                    ModelState.AddModelError("Photo", "image size 2MB");
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || id == 0) return NotFound();
                Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
                if (slider == null) return NotFound();
                return View(slider);
            }

            [HttpPost]
            [AutoValidateAntiforgeryToken]
            public async Task<IActionResult> Edit(int? id, Slider slider)
            {
                if (id == null || id == 0) return NotFound();
                Slider existed = await _context.Sliders.FindAsync(id);
                if (existed == null) return NotFound();
                if (!ModelState.IsValid) return View(slider);
                if (slider.Photo == null)
                {
                    string filename = existed.Image;
                    _context.Entry(existed).CurrentValues.SetValues(slider);
                    existed.Image = filename;
                }
                else
                {
                    if (!slider.Photo.ImageIsOkay(3))
                    {
                        ModelState.AddModelError("Photo", "choose image file");
                        return View(existed);
                    }
                    FileValidator.FileDelete(_env.WebRootPath, "assets/image", existed.Image);
                    _context.Entry(existed).CurrentValues.SetValues(slider);
                    existed.Image = await slider.Photo.FileCreate(_env.WebRootPath, "assets/image");
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            public async Task<IActionResult> Detail(int? id)
            {
                if (id == null || id == 0) return NotFound();
                Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
                if (slider == null) return NotFound();
                return View(slider);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || id == 0) return NotFound();
                Slider slider = await _context.Sliders.FindAsync(id);
                if (slider == null) return NotFound();
                _context.Sliders.Remove(slider);
                var rootpath = Path.Combine(_env.WebRootPath, "assets/image/", slider.Image);
                System.IO.File.Delete(rootpath);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
    }
}
