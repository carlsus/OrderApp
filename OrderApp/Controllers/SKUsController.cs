using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderApp.Models;

namespace OrderApp.Controllers
{
    public class SKUsController : Controller
    {
        private readonly OrderDBContext _context;
        private readonly IWebHostEnvironment _env;
        public SKUsController(OrderDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: SKUs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skus.ToListAsync());
        }

        // GET: SKUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sKU = await _context.Skus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sKU == null)
            {
                return NotFound();
            }

            return View(sKU);
        }

        // GET: SKUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SKUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SKU sku,IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null || Image.Length == 0)
                    return Json(new { success = false, message = "No file selected." });

                
                string uploadPath = Path.Combine(_env.WebRootPath, "images");

                
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                sku.ImagePath = $"/images/{fileName}";
                _context.Add(sku);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "SKU created successfully." });
            }
            var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
            return Json(new { success = false, errors });
        }

        // GET: SKUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sKU = await _context.Skus.FindAsync(id);
            if (sKU == null)
            {
                return NotFound();
            }
            return View(sKU);
        }

        // POST: SKUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromForm] SKU sKU, IFormFile Image)
        {
            if (id != sKU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var sku = _context.Skus.FirstOrDefault(c => c.Id == id);
                    if (Image == null || Image.Length == 0)
                    return Json(new { success = false, message = "No file selected." });

                    string uploadPath = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    // Delete old image kung meron
                    if (!string.IsNullOrEmpty(sku.ImagePath))
                    {
                        string oldImagePath = Path.Combine(uploadPath, sku.ImagePath);
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);
                    }

                    // Save new image
                    string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    string newFilePath = Path.Combine(uploadPath, newFileName);

                    using (var stream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    sKU.ImagePath = $"/images/{newFileName}";
                    var local = _context.Skus.Local.FirstOrDefault(c => c.Id == sKU.Id);
                    if (local != null)
                    {
                        _context.Entry(local).State = EntityState.Detached;
                    }

                    _context.Attach(sKU);
                    _context.Entry(sKU).State = EntityState.Modified;
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return Json(new { success = true, message = "Update  successfully." });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        
    }
}
