// ImageModel.cs (داخل Models)
using BestStoreMVC.Migrations;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using sib_api_v3_sdk.Model;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System;
using BestStoreMVC.Services;
using BestStoreMVC.Models;

public class ImageModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

// GalleryDbContext.cs (داخل Data)



public class ImagesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ImagesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var images = await _context.ImageModels.OrderByDescending(i => i.CreatedAt).ToListAsync();
        return View(images);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile File, string Title, string Description)
    {
        if (File == null || File.Length == 0)
            return RedirectToAction("Index");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await File.CopyToAsync(stream);
        }

        var image = new ImageModel
        {
            Title = Title,
            Description = Description,
            Url = "/uploads/" + uniqueName,
            CreatedAt = DateTime.Now
        };

        _context.ImageModels.Add(image);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var image = await _context.ImageModels.FindAsync(id);
        if (image == null) return NotFound();

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.Url.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);

        _context.ImageModels.Remove(image);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}