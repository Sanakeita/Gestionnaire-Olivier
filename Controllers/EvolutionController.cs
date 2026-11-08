using System.Text.Json;
using GestionOliviers.Data;
using GestionOliviers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionOliviers.Controllers;

public class EvolutionController : Controller
{
    private readonly OliviersContext _context;

    public EvolutionController(OliviersContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? anneeDebut, int? anneeFin, int? champId)
    {
        var minAnnee = await _context.Recoltes.MinAsync(r => (int?)r.Annee) ?? DateTime.Now.Year - 5;
        var maxAnnee = await _context.Recoltes.MaxAsync(r => (int?)r.Annee) ?? DateTime.Now.Year;

        var filtre = new EvolutionFiltre
        {
            AnneeDebut = anneeDebut ?? minAnnee,
            AnneeFin = anneeFin ?? maxAnnee,
            ChampId = champId
        };

        if (filtre.AnneeDebut > filtre.AnneeFin)
        {
            (filtre.AnneeDebut, filtre.AnneeFin) = (filtre.AnneeFin, filtre.AnneeDebut);
        }

        var query = _context.Recoltes
            .Include(r => r.Olivier)!
            .ThenInclude(o => o!.Champ)
            .Where(r => r.Annee >= filtre.AnneeDebut && r.Annee <= filtre.AnneeFin);

        if (filtre.ChampId is not null)
        {
            query = query.Where(r => r.Olivier!.ChampId == filtre.ChampId);
        }

        var recoltes = await query.AsNoTracking().ToListAsync();

        var lignes = recoltes
            .GroupBy(r => new { r.Annee, Champ = r.Olivier!.Champ!.Nom, ChampId = r.Olivier.ChampId })
            .Select(g => new
            {
                g.Key.Annee,
                g.Key.Champ,
                g.Key.ChampId,
                TotalKg = g.Sum(x => (double)x.QuantiteKg),
                NbOliviers = g.Select(x => x.OlivierId).Distinct().Count()
            })
            .OrderBy(x => x.Annee)
            .ThenBy(x => x.Champ)
            .ToList();

        var annees = Enumerable.Range(filtre.AnneeDebut, filtre.AnneeFin - filtre.AnneeDebut + 1).ToList();
        var champs = lignes.Select(l => l.Champ).Distinct().OrderBy(n => n).ToList();

        var series = champs.Select(champ => new
        {
            label = champ,
            data = annees.Select(annee =>
                lignes.Where(l => l.Champ == champ && l.Annee == annee)
                    .Select(l => l.TotalKg)
                    .FirstOrDefault())
                .ToList()
        });

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        ViewBag.Filtre = filtre;
        ViewBag.Champs = new SelectList(await _context.Champs.OrderBy(c => c.Nom).ToListAsync(), "Id", "Nom", filtre.ChampId);
        ViewBag.Annees = annees;
        ViewBag.SeriesJson = JsonSerializer.Serialize(series, jsonOptions);
        ViewBag.AnneesJson = JsonSerializer.Serialize(annees, jsonOptions);
        ViewBag.Lignes = lignes;
        ViewBag.TotalPeriode = lignes.Sum(l => l.TotalKg);
        ViewBag.MoyenneAnnuelle = annees.Count == 0
            ? 0
            : lignes.Sum(l => l.TotalKg) / annees.Count;

        return View();
    }
}
