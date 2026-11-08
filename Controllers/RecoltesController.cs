using GestionOliviers.Data;
using GestionOliviers.Localization;
using GestionOliviers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionOliviers.Controllers;

public class RecoltesController : Controller
{
    private readonly OliviersContext _context;
    private readonly UiLocalizer _texts;

    public RecoltesController(OliviersContext context, UiLocalizer texts)
    {
        _context = context;
        _texts = texts;
    }

    public async Task<IActionResult> Index(int? annee, int? champId)
    {
        var query = _context.Recoltes
            .Include(r => r.Olivier)!
            .ThenInclude(o => o!.Champ)
            .AsQueryable();

        if (annee is not null)
        {
            query = query.Where(r => r.Annee == annee);
        }

        if (champId is not null)
        {
            query = query.Where(r => r.Olivier!.ChampId == champId);
        }

        ViewBag.Annee = annee;
        ViewBag.ChampId = champId;
        ViewBag.Annees = new SelectList(
            await _context.Recoltes.Select(r => r.Annee).Distinct().OrderByDescending(a => a).ToListAsync(),
            annee);
        ViewBag.Champs = new SelectList(await _context.Champs.OrderBy(c => c.Nom).ToListAsync(), "Id", "Nom", champId);

        var recoltes = await query
            .OrderByDescending(r => r.Annee)
            .ThenBy(r => r.Olivier!.Code)
            .ToListAsync();

        return View(recoltes);
    }

    public async Task<IActionResult> Create()
    {
        await FillOliviers();
        return View(new Recolte { Annee = DateTime.Now.Year, DateRecolte = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Recolte recolte)
    {
        await ValidateUniqueHarvest(recolte);

        if (!ModelState.IsValid)
        {
            await FillOliviers(recolte.OlivierId);
            return View(recolte);
        }

        _context.Add(recolte);
        await _context.SaveChangesAsync();
        TempData["Message"] = _texts["Récolte enregistrée (une récolte par olivier et par an)."];
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var recolte = await _context.Recoltes.FindAsync(id);
        if (recolte is null)
        {
            return NotFound();
        }

        await FillOliviers(recolte.OlivierId);
        return View(recolte);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Recolte recolte)
    {
        if (id != recolte.Id)
        {
            return NotFound();
        }

        await ValidateUniqueHarvest(recolte);

        if (!ModelState.IsValid)
        {
            await FillOliviers(recolte.OlivierId);
            return View(recolte);
        }

        _context.Update(recolte);
        await _context.SaveChangesAsync();
        TempData["Message"] = _texts["Récolte mise à jour."];
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var recolte = await _context.Recoltes
            .Include(r => r.Olivier)!
            .ThenInclude(o => o!.Champ)
            .FirstOrDefaultAsync(r => r.Id == id);
        return recolte is null ? NotFound() : View(recolte);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var recolte = await _context.Recoltes.FindAsync(id);
        if (recolte is not null)
        {
            _context.Recoltes.Remove(recolte);
            await _context.SaveChangesAsync();
            TempData["Message"] = _texts["Récolte supprimée."];
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task ValidateUniqueHarvest(Recolte recolte)
    {
        var exists = await _context.Recoltes.AnyAsync(r =>
            r.OlivierId == recolte.OlivierId &&
            r.Annee == recolte.Annee &&
            r.Id != recolte.Id);

        if (exists)
        {
            ModelState.AddModelError(nameof(recolte.Annee), _texts["Cet olivier a déjà une récolte pour cette année."]);
        }
    }

    private async Task FillOliviers(int? selected = null)
    {
        var oliviers = await _context.Oliviers
            .Include(o => o.Champ)
            .OrderBy(o => o.Champ!.Nom)
            .ThenBy(o => o.Code)
            .ToListAsync();

        ViewBag.OlivierId = new SelectList(
            oliviers.Select(o => new { o.Id, Label = $"{o.Code} — {o.Champ?.Nom}" }),
            "Id",
            "Label",
            selected);
    }
}
