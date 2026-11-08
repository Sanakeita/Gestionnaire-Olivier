using GestionOliviers.Data;
using GestionOliviers.Localization;
using GestionOliviers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionOliviers.Controllers;

public class ChampsController : Controller
{
    private readonly OliviersContext _context;
    private readonly UiLocalizer _texts;

    public ChampsController(OliviersContext context, UiLocalizer texts)
    {
        _context = context;
        _texts = texts;
    }

    public async Task<IActionResult> Index()
    {
        var champs = await _context.Champs
            .Include(c => c.Oliviers)
            .OrderBy(c => c.Nom)
            .ToListAsync();
        return View(champs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var champ = await _context.Champs
            .Include(c => c.Oliviers)
            .ThenInclude(o => o.Recoltes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (champ is null)
        {
            return NotFound();
        }

        return View(champ);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Champ champ)
    {
        if (!ModelState.IsValid)
        {
            return View(champ);
        }

        _context.Add(champ);
        await _context.SaveChangesAsync();
        TempData["Message"] = _texts["Champ enregistré."];
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var champ = await _context.Champs.FindAsync(id);
        return champ is null ? NotFound() : View(champ);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Champ champ)
    {
        if (id != champ.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(champ);
        }

        _context.Update(champ);
        await _context.SaveChangesAsync();
        TempData["Message"] = _texts["Champ mis à jour."];
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var champ = await _context.Champs
            .Include(c => c.Oliviers)
            .FirstOrDefaultAsync(c => c.Id == id);
        return champ is null ? NotFound() : View(champ);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var champ = await _context.Champs.FindAsync(id);
        if (champ is not null)
        {
            _context.Champs.Remove(champ);
            await _context.SaveChangesAsync();
            TempData["Message"] = _texts["Champ supprimé."];
        }

        return RedirectToAction(nameof(Index));
    }
}
