using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerAddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerAddressesController> _logger;

        public CustomerAddressesController(ApplicationDbContext context, ILogger<CustomerAddressesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: CustomerAddresses/Create?customerId=1
        public IActionResult Create(long customerId)
        {
            var model = new CustomerAddress { CustomerId = customerId };
            return View(model);
        }

        // POST: CustomerAddresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,AddressLine,City,State,PostalCode,Country,IsPrimary")] CustomerAddress address)
        {
            if (ModelState.IsValid)
            {
                if (address.IsPrimary)
                {
                    // unset other primary flags
                    var others = _context.CustomerAddresses.Where(a => a.CustomerId == address.CustomerId && a.IsPrimary);
                    foreach (var o in others) o.IsPrimary = false;
                }
                _context.Add(address);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Address added for Customer {address.CustomerId}");
                return RedirectToAction("Details", "Customers", new { id = address.CustomerId });
            }
            return View(address);
        }

        // GET: CustomerAddresses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();
            var address = await _context.CustomerAddresses.FindAsync(id);
            if (address == null) return NotFound();
            return View(address);
        }

        // POST: CustomerAddresses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CustomerId,AddressLine,City,State,PostalCode,Country,IsPrimary")] CustomerAddress address)
        {
            if (id != address.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    if (address.IsPrimary)
                    {
                        var others = _context.CustomerAddresses.Where(a => a.CustomerId == address.CustomerId && a.IsPrimary && a.Id != address.Id);
                        foreach (var o in others) o.IsPrimary = false;
                    }
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Address updated: {address.Id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.CustomerAddresses.Any(e => e.Id == address.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction("Details", "Customers", new { id = address.CustomerId });
            }
            return View(address);
        }

        // GET: CustomerAddresses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();
            var address = await _context.CustomerAddresses.Include(a => a.Customer).FirstOrDefaultAsync(a => a.Id == id);
            if (address == null) return NotFound();
            return View(address);
        }

        // POST: CustomerAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var address = await _context.CustomerAddresses.FindAsync(id);
            if (address != null)
            {
                var custId = address.CustomerId;
                _context.CustomerAddresses.Remove(address);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Address deleted: {id}");
                return RedirectToAction("Details", "Customers", new { id = custId });
            }
            return RedirectToAction("Index", "Customers");
        }
    }
}
