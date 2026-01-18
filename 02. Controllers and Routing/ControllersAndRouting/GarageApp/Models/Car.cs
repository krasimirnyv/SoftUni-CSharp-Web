namespace GarageApp.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Enums;
using static Common.EntityValidation;

public class Car
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(CarMakeMaxLength)]
    public string Make { get; set; } = null!;
    
    [Required]
    [MaxLength(CarModelMaxLength)]
    public string Model { get; set; } = null!;
    
    [Range(1886, 2100)]
    public int Year { get; set; }
    
    [Required]
    public CarBodyStyle BodyStyle { get; set; }
    
    public bool IsAvailable { get; set; }
    
    [Required]
    [ForeignKey(nameof(Garage))]
    public Guid GarageId { get; set; }
    
    public virtual Garage Garage { get; set; } = null!;

}
