using FinalProjectMedimall.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using FinalProjectMedimall.DAL;
using System.Linq;
using FinalProjectMedimall.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{

    [Area("Medimalladmin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 5);
            ViewBag.CurrentPage = page;
            List<Category> model = await _context.Categories.Skip((page - 1) * 5).Take(5).ToListAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            bool dublicate=_context.Categories.Any(c=>c.Name==category.Name);
            if (dublicate)
            {
                ModelState.AddModelError("Name", "can not dubilcate name");
                return View();
            }
            if (category.Photo == null || category.Name==null)
            {
                ModelState.AddModelError("Photo", "Please choose picture");
                return View();
            }
            if (!category.Photo.ImageIsOkay(2))
            {
                ModelState.AddModelError("Photo", "Please coho0se correct picture");
                return View();
            }
            category.Image = await category.Photo.FileCreate(_env.WebRootPath, "assets/image/");
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, Category NewCategory)
        {
            if (id == null || id == 0) return NotFound();
            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (!ModelState.IsValid) return View(existed);
            if (existed == null) return NotFound();
            bool RepeatCategory = _context.Categories.Any(c => c.Id != existed.Id && c.Name == NewCategory.Name);
            if (RepeatCategory)
            {
                ModelState.AddModelError("Name", "can not dubilcate name");
                return View();
            }
            if (NewCategory.Photo == null)
            {
                string filename = existed.Image;
                _context.Entry(existed).CurrentValues.SetValues(NewCategory);
                existed.Image = filename;
            }
            else
            {
                if (!NewCategory.Photo.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Image", "choose valid image");
                    return View();
                }
                FileValidator.FileDelete(_env.WebRootPath, "assets/image/", existed.Image);
                _context.Entry(existed).CurrentValues.SetValues(NewCategory);
                existed.Image = await FileValidator.FileCreate(NewCategory.Photo, _env.WebRootPath, "assets/image/");
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            var rootpath = Path.Combine(_env.WebRootPath, "assets/image/", category.Image);
            System.IO.File.Delete(rootpath);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
