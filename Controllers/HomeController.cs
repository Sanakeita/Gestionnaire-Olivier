using System.Diagnostics;
using GestionOliviers.Data;
using GestionOliviers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionOliviers.Controllers;

public class HomeController : Controller
{
    private readonly OliviersContext _context;

    public HomeController(OliviersContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.NbChamps = await _context.Champs.CountAsync();
        ViewBag.NbOliviers = await _context.Oliviers.CountAsync();
        ViewBag.NbRecoltes = await _context.Recoltes.CountAsync();
        ViewBag.TotalKg = await _context.Recoltes.SumAsync(r => (double?)r.QuantiteKg) ?? 0;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
