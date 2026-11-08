using System.ComponentModel.DataAnnotations;

namespace GestionOliviers.Models;

public class EvolutionFiltre
{
    [Display(Name = "Année de début")]
    public int AnneeDebut { get; set; }

    [Display(Name = "Année de fin")]
    public int AnneeFin { get; set; }

    [Display(Name = "Champ")]
    public int? ChampId { get; set; }
}
