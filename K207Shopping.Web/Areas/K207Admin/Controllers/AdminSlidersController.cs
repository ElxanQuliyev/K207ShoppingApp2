using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K207Shopping.Web.Data;
using K207Shopping.Web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace K207Shopping.Web.Areas.K207Admin.Controllers
{
    [Area("K207Admin")]
    public class AdminSlidersController : Controller
    {
        private readonly ShoppingContext _context;
        private readonly IWebHostEnvironment _environment;


        public AdminSlidersController(ShoppingContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: K207Admin/AdminSliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        // GET: K207Admin/AdminSliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: K207Admin/AdminSliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: K207Admin/AdminSliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PhotoUrl,SubHeader,Header,Description")] Slider slider,IFormFile PhotoUrl)
        {
            if (ModelState.IsValid)
            {
                if (PhotoUrl != null)
                {
                    string filename = Guid.NewGuid() + PhotoUrl.FileName;
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    string imageFolder = Path.Combine(uploadFolder, filename);
                    using FileStream fileStream = new FileStream(imageFolder, FileMode.Create);
                    await PhotoUrl.CopyToAsync(fileStream);
                    slider.PhotoUrl = filename;
                }
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: K207Admin/AdminSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: K207Admin/AdminSliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PhotoUrl,SubHeader,Header,Description")] Slider slider,IFormFile PhotoUrl)
        {
            if (id != slider.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (PhotoUrl != null)
                    {
                        string filename = Guid.NewGuid() + PhotoUrl.FileName;
                        string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");
                        string imageFolder = Path.Combine(uploadFolder, filename);
                        using FileStream fileStream = new FileStream(imageFolder, FileMode.Create);
                        await PhotoUrl.CopyToAsync(fileStream);
                        var oldPicture = Path.Combine(uploadFolder, slider.PhotoUrl);
                        if (System.IO.File.Exists(Path.Combine(oldPicture)))
                        {
                            System.IO.File.Delete(oldPicture);
                        }
                        slider.PhotoUrl = filename;
                    }
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: K207Admin/AdminSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: K207Admin/AdminSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            return _context.Sliders.Any(e => e.ID == id);
        }
    }
}
