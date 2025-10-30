using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApp.Models;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly OrderDBContext _context;

        public CustomersController(OrderDBContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
          
            if (ModelState.IsValid)
            {
               
                _context.Add(customer);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Customer created successfully." });
            }
            var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
            return Json(new { success = false, errors });
            
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var local = _context.Customers.Local.FirstOrDefault(c => c.Id == customer.Id);
                    if (local != null)
                    {
                        _context.Entry(local).State = EntityState.Detached;
                    }

                    _context.Attach(customer);
                    _context.Entry(customer).State = EntityState.Modified;
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
