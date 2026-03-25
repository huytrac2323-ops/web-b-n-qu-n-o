using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.Data;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;

    public AdminController(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    // Index - Fetch Agents from database
    public IActionResult Index()
    {
        var agents = _db.Agents.OrderBy(a => a.Id).ToList();
        return View(agents);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return View(product);

        if (imageFile != null && imageFile.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(uploads);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploads, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await imageFile.CopyToAsync(stream);
            }
            product.Image = "/images/" + fileName;
        }

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var p = _db.Products.FirstOrDefault(x => x.Id == id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
            return View(product);

        var existing = _db.Products.FirstOrDefault(x => x.Id == product.Id);
        if (existing == null) return NotFound();

        existing.Name = product.Name;
        existing.Price = product.Price;

        if (imageFile != null && imageFile.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(uploads);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploads, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await imageFile.CopyToAsync(stream);
            }
            existing.Image = "/images/" + fileName;
        }

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var p = _db.Products.FirstOrDefault(x => x.Id == id);
        if (p != null)
        {
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
