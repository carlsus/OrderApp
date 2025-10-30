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

        public SKUsController(OrderDBContext context)
        {
            _context = context;
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
