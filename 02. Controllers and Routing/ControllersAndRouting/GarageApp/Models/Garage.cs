namespace GarageApp.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidation;

public class Garage
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(GarageNameMaxLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    [MaxLength(GarageLocationMaxLength)]
    public string Location { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; }
        = new HashSet<Car>();
}
