using GestionOliviers.Data;
using GestionOliviers.Localization;
using GestionOliviers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionOliviers.Controllers;

public class OliviersController : Controller
{
    private readonly OliviersContext _context;
    private readonly UiLocalizer _texts;

    public OliviersController(OliviersContext context, UiLocalizer texts)
    {
        _context = context;
        _texts = texts;
    }

    public async Task<IActionResult> Index(int? champId)
    {
        var query = _context.Oliviers.Include(o => o.Champ).AsQueryable();
        if (champId is not null)
        {
            query = query.Where(o => o.ChampId == champId);
        }

        ViewBag.ChampId = champId;
        ViewBag.Champs = new SelectList(await _context.Champs.OrderBy(c => c.Nom).ToListAsync(), "Id", "Nom", champId);
        return View(await query.OrderBy(o => o.Code).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var olivier = await _context.Oliviers
            .Include(o => o.Champ)
            .Include(o => o.Recoltes)
            .FirstOrDefaultAsync(o => o.Id == id);

        return olivier is null ? NotFound() : View(olivier);
    }

    public async Task<IActionResult> Create()
    {
        await FillChamps();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Olivier olivier)
    {
        if (await _context.Oliviers.AnyAsync(o => o.Code == olivier.Code))
        {
            ModelState.AddModelError(nameof(olivier.Code), _texts["Cet identifiant d'olivier existe déjà."]);
        }

        if (!ModelState.IsValid)
        {
            await FillChamps(olivier.ChampId);
            return View(olivier);
        }

        _context.Add(olivier);
        await _context.SaveChangesAsync();
        TempData["Message"] = _texts["Olivier enregistré."];
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var olivier = await _context.Oliviers.FindAsync(id);
        if (olivier is null)
        {
            return NotFound();
        }

        await FillChamps(olivier.ChampId);
        return View(olivier);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Olivier olivier)
    {
        if (id != olivier.Id)
        {
            return NotFound();
        }

        if (await _context.Oliviers.AnyAsync(o => o.Code == olivier.Code && o.Id != olivier.Id))
        {
            ModelState.AddModelError(nameof(olivier.Code), _texts["Cet identifiant d'olivier existe déjà."]);
        }

        if (!ModelState.IsValid)
        {
            await FillChamps(olivier.ChampId);
            return View(olivier);
        }

        _context.Update(olivier);
        await _context.SaveChangesAsync();
        TempData["Message"] = _texts["Olivier mis à jour."];
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var olivier = await _context.Oliviers
            .Include(o => o.Champ)
            .FirstOrDefaultAsync(o => o.Id == id);
        return olivier is null ? NotFound() : View(olivier);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var olivier = await _context.Oliviers.FindAsync(id);
        if (olivier is not null)
        {
            _context.Oliviers.Remove(olivier);
            await _context.SaveChangesAsync();
            TempData["Message"] = _texts["Olivier supprimé."];
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task FillChamps(int? selected = null)
    {
        ViewBag.ChampId = new SelectList(await _context.Champs.OrderBy(c => c.Nom).ToListAsync(), "Id", "Nom", selected);
    }
}
