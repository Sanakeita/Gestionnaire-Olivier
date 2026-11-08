using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GestionOliviers.Models;

[Index(nameof(OlivierId), nameof(Annee), IsUnique = true)]
public class Recolte
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Olivier")]
    public int OlivierId { get; set; }

    public Olivier? Olivier { get; set; }

    [Required]
    [Display(Name = "Année")]
    [Range(1990, 2100, ErrorMessage = "L'année doit être comprise entre 1990 et 2100.")]
    public int Annee { get; set; }

    [Required]
    [Display(Name = "Quantité (kg)")]
    [Range(0, 10000, ErrorMessage = "La quantité doit être positive.")]
    public decimal QuantiteKg { get; set; }

    [Display(Name = "Date de récolte")]
    [DataType(DataType.Date)]
    public DateTime? DateRecolte { get; set; }

    [Display(Name = "Qualité")]
    [StringLength(40)]
    public string? Qualite { get; set; }
}
