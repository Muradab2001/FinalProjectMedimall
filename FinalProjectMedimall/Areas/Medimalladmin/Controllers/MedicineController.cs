using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using FinalProjectMedimall.DAL;
using FinalProjectMedimall.Models;
using System.Linq;
using FinalProjectMedimall.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMedimall.Areas.Medimalladmin.Controllers
{

    [Area("Medimalladmin")]
    public class MedicineController : Controller
    {
            private readonly ApplicationDbContext _context;
            private readonly IWebHostEnvironment _env;

            public MedicineController(ApplicationDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }
            public async Task<IActionResult> Index()
            {
                List<Medicine> model = await _context.Medicines
                .Include(c => c.Category)
                .Include(c => c.MedicineImages).ToListAsync();
                return View(model);
            }

            public IActionResult Create()
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View();
            }

            [HttpPost]
            [AutoValidateAntiforgeryToken]
            public async Task<IActionResult> Create(Medicine medicine)
            {
                ViewBag.Categories = _context.Categories.ToList();
                if (!ModelState.IsValid) return View();
                if (medicine.MainPhoto == null || medicine.Photos == null)
                {
                    ModelState.AddModelError(string.Empty, "must choose 1 main photo");
                    return View();
                }
                if (!medicine.MainPhoto.ImageIsOkay(2))
                {
                    ModelState.AddModelError(string.Empty, "choose image file");
                    return View();
                }
             medicine.Image = await FileValidator.FileCreate(medicine.MainPhoto, _env.WebRootPath, "assets/image");
             medicine.MedicineImages = new List<MedicineImage>();
                TempData["Filename"] = null;
                List<IFormFile> removeable = new List<IFormFile>();
                foreach (var photo in medicine.Photos.ToList())
                {
                    if (!photo.ImageIsOkay(2))
                    {
                        removeable.Add(photo);
                        TempData["Filename"] += photo.FileName + ",";
                        continue;
                    }
                    MedicineImage otherphoto = new MedicineImage
                    {
                        Name = await photo.FileCreate(_env.WebRootPath, "assets/image"),
                        IsMain = false,
                        Alternative = medicine.Name,
                        Medicine = medicine
                    };
                    medicine.MedicineImages.Add(otherphoto);
                }
                medicine.MedicineImages.RemoveAll(c => removeable.Any(f => f.FileName == f.FileName));
                MedicineImage main = new MedicineImage
                {
                    Name = medicine.Image,
                    IsMain = true,
                    Alternative = medicine.Name,
                    Medicine = medicine
                };
                medicine.MedicineImages.Add(main);

                await _context.Medicines.AddAsync(medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Edit(int? id)
            {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.images = _context.MedicineImages.ToList();
            if (id == 0 || id == null) return NotFound();
                if (!ModelState.IsValid) return View();
                Medicine medicine = await _context.Medicines
                .Include(c => c.MedicineImages)
                .Include(c => c.Category).SingleOrDefaultAsync(c => c.Id == id);
                if (medicine == null) return NotFound();
            return View(medicine);
            }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, Medicine medicine)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.images = _context.MedicineImages.ToList();
            Medicine existed = _context.Medicines.Include(m => m.MedicineImages).Include(m => m.Category).FirstOrDefault(m => m.Id == id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (medicine.ImagesId == null && medicine.Photos == null)
            {
                ModelState.AddModelError("Photos", "must choose 1 main photo");
                return View(existed);
            }
            TempData["FileName"] = default(string);

            if (medicine.Photos != null)
            {
                if (medicine.ImagesId is null)
                {
                    foreach (MedicineImage medicineImage in existed.MedicineImages.Where(m => m.IsMain == false))
                    {
                        FileValidator.FileDelete(_env.WebRootPath, "assets/image", medicineImage.Name);
                    }
                    existed.MedicineImages.RemoveAll(p => p.IsMain == false);
                }
                else
                {
                    List<MedicineImage> medicineImages = existed.MedicineImages
                      .Where(p => p.IsMain == false && !medicine.ImagesId
                      .Contains(p.Id)).ToList();

                    existed.MedicineImages
                        .RemoveAll(p => medicineImages.Any(r => p.Id == r.Id));
                }
                 foreach (IFormFile image in medicine.Photos.ToList())
            {
                if (medicine.Photos.Count == 0)
                {
                    ModelState.AddModelError("Photos", "None of the detail images are valid type");
                    return View(existed);
                }

                if (!image.ImageIsOkay(2))
                {
                    medicine.Photos.Remove(image);
                    TempData["FileName"] += image.FileName + ",";
                    continue;
                }
                MedicineImage photos = new MedicineImage
                {
                    Name = await image.FileCreate(_env.WebRootPath,
                        "assets/image"),
                    IsMain = false,
                    Medicine = existed,
                    Alternative = medicine.Name,
                };
                await _context.MedicineImages.AddAsync(photos);
            }
            
            }
            else
            {
                if (medicine.ImagesId is null)
                {
                    foreach (MedicineImage medicineImage in existed.MedicineImages.Where(m => m.IsMain == false))
                    {
                        FileValidator.FileDelete(_env.WebRootPath, "assets/image", medicineImage.Name);
                    }
                    existed.MedicineImages.RemoveAll(p => p.IsMain == false);
                }
                else
                {
                    List<MedicineImage> medicineImages = existed.MedicineImages
                      .Where(p => p.IsMain == false && !medicine.ImagesId
                      .Contains(p.Id)).ToList();
                    foreach (MedicineImage  totalImage in medicineImages)
                    {
                        FileValidator.FileDelete(_env.WebRootPath, "assets/image", totalImage.Name);
                    }
                    existed.MedicineImages
                        .RemoveAll(p => medicineImages.Any(r => p.Id == r.Id));
                }
            }
            if (medicine.MainPhoto != null)
            {
                if (!medicine.MainPhoto.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Photos", "Please choose valid image file");
                    return View(existed);
                }

                MedicineImage main = new MedicineImage
                {
                    IsMain = true,
                    Alternative = medicine.Name,
                    Name = await medicine.MainPhoto
                    .FileCreate(_env.WebRootPath,
                    "assets/image"),
                    Medicine = existed,
                };
                medicine.Image = main.Name;
                string mainPhoto = existed.MedicineImages
                    .FirstOrDefault(p => p.IsMain == true).Name;
                FileValidator.FileDelete(_env.WebRootPath,
                    "assets/image", mainPhoto);
                existed.MedicineImages.FirstOrDefault(p => p.IsMain == true).Name = main.Name;
                existed.MedicineImages.FirstOrDefault(p => p.IsMain == true)
                    .Alternative = main.Alternative;
            }
            else
            {
                medicine.Image = existed.Image;
            }
            _context.Entry(existed).CurrentValues.SetValues(medicine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        public async Task<IActionResult> Detail(int? id)
            {
                if (id == null || id == 0) return NotFound();
                Medicine medicine = await _context.Medicines.FirstOrDefaultAsync(c => c.Id == id);
                if (medicine == null) return NotFound();
                return View(medicine);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || id == 0) return NotFound();
                Medicine medicine = await _context.Medicines.FindAsync(id);
                 if (medicine == null) return NotFound();
            List<MedicineImage> medicineImages = await _context.MedicineImages.ToListAsync();
                foreach (MedicineImage item in medicineImages)
                {
                    if (medicine.Id == item.MedicineId)
                    {
                        var alternativpath = Path.Combine(_env.WebRootPath, "assets/image", item.Name);
                        System.IO.File.Delete(alternativpath);
                    }
                }
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }

