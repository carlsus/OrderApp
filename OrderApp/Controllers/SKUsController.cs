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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code,UnitPrice,DateCreated,CreatedBy,Timestamp,UserId,IsActive,ImagePath")] SKU sKU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sKU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sKU);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,UnitPrice,DateCreated,CreatedBy,Timestamp,UserId,IsActive,ImagePath")] SKU sKU)
        {
            if (id != sKU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sKU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SKUExists(sKU.Id))
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
            return View(sKU);
        }

        // GET: SKUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: SKUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sKU = await _context.Skus.FindAsync(id);
            if (sKU != null)
            {
                _context.Skus.Remove(sKU);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SKUExists(int id)
        {
            return _context.Skus.Any(e => e.Id == id);
        }
    }
}
