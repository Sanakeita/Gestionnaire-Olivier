using GestionOliviers.Models;

namespace GestionOliviers.Data;

public static class DbInitializer
{
    public static void Seed(OliviersContext context)
    {
        if (context.Champs.Any())
        {
            return;
        }

        var nord = new Champ
        {
            Nom = "Coteau Nord",
            Localisation = "Colline de Sidi Bou Saïd",
            SuperficieHa = 2.4m,
            Notes = "Exposition sud-est, sol calcaire."
        };
        var vallee = new Champ
        {
            Nom = "Vallée des Oliviers",
            Localisation = "Plaine de Mornag",
            SuperficieHa = 4.1m,
            Notes = "Irrigation goutte-à-goutte."
        };
        var plateau = new Champ
        {
            Nom = "Plateau Ouest",
            Localisation = "Hauteurs de Zaghouan",
            SuperficieHa = 3.0m,
            Notes = "Variétés anciennes, conduite traditionnelle."
        };

        context.Champs.AddRange(nord, vallee, plateau);
        context.SaveChanges();

        var oliviers = new List<Olivier>
        {
            new() { Code = "N-01", Variete = "Chemlali", AnneePlantation = 1998, ChampId = nord.Id },
            new() { Code = "N-02", Variete = "Chetoui", AnneePlantation = 2004, ChampId = nord.Id },
            new() { Code = "N-03", Variete = "Chemlali", AnneePlantation = 2001, ChampId = nord.Id },
            new() { Code = "V-01", Variete = "Arbequina", AnneePlantation = 2012, ChampId = vallee.Id },
            new() { Code = "V-02", Variete = "Chemlali", AnneePlantation = 2008, ChampId = vallee.Id },
            new() { Code = "V-03", Variete = "Picholine", AnneePlantation = 2010, ChampId = vallee.Id },
            new() { Code = "V-04", Variete = "Chetoui", AnneePlantation = 2006, ChampId = vallee.Id },
            new() { Code = "P-01", Variete = "Meski", AnneePlantation = 1995, ChampId = plateau.Id },
            new() { Code = "P-02", Variete = "Chemlali", AnneePlantation = 1992, ChampId = plateau.Id },
            new() { Code = "P-03", Variete = "Zarazi", AnneePlantation = 2000, ChampId = plateau.Id },
            new() { Code = "N-04", Variete = "Oueslati", AnneePlantation = 2016, ChampId = nord.Id },
            new() { Code = "V-05", Variete = "Sayali", AnneePlantation = 2018, ChampId = vallee.Id },
            new() { Code = "P-04", Variete = "Zalmati", AnneePlantation = 2014, ChampId = plateau.Id }
        };

        context.Oliviers.AddRange(oliviers);
        context.SaveChanges();

        var random = new Random(42);
        var qualites = new[] { "Extra", "Vierge", "Courante" };
        var recoltes = new List<Recolte>();

        foreach (var olivier in oliviers)
        {
            for (var annee = 2020; annee <= 2026; annee++)
            {
                var baseKg = olivier.ChampId == vallee.Id ? 38 : olivier.ChampId == nord.Id ? 32 : 28;
                var variation = (decimal)(random.NextDouble() * 12 - 4);
                var tendance = (annee - 2020) * 1.4m;
                recoltes.Add(new Recolte
                {
                    OlivierId = olivier.Id,
                    Annee = annee,
                    QuantiteKg = Math.Round(baseKg + variation + tendance, 1),
                    DateRecolte = new DateTime(annee, 11, random.Next(5, 26)),
                    Qualite = qualites[random.Next(qualites.Length)]
                });
            }
        }

        context.Recoltes.AddRange(recoltes);
        context.SaveChanges();
    }
}
