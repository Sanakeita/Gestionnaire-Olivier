using System.ComponentModel.DataAnnotations;

namespace GestionOliviers.Models;

public class Olivier
{
    public int Id { get; set; }

    [Required(ErrorMessage = "L'identifiant de l'olivier est obligatoire.")]
    [Display(Name = "Identifiant")]
    [StringLength(40)]
    public string Code { get; set; } = string.Empty;

    [Display(Name = "Variété")]
    [StringLength(80)]
    public string? Variete { get; set; }

    [Display(Name = "Année de plantation")]
    [Range(1900, 2100)]
    public int? AnneePlantation { get; set; }

    [Required]
    [Display(Name = "Champ")]
    public int ChampId { get; set; }

    public Champ? Champ { get; set; }

    public ICollection<Recolte> Recoltes { get; set; } = new List<Recolte>();
}
