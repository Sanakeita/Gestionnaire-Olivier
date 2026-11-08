using System.ComponentModel.DataAnnotations;

namespace GestionOliviers.Models;

public class Champ
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom du champ est obligatoire.")]
    [Display(Name = "Nom du champ")]
    [StringLength(120)]
    public string Nom { get; set; } = string.Empty;

    [Display(Name = "Localisation")]
    [StringLength(200)]
    public string? Localisation { get; set; }

    [Display(Name = "Superficie (ha)")]
    [Range(0.01, 10000)]
    public decimal SuperficieHa { get; set; }

    [Display(Name = "Notes")]
    [StringLength(500)]
    public string? Notes { get; set; }

    public ICollection<Olivier> Oliviers { get; set; } = new List<Olivier>();
}
