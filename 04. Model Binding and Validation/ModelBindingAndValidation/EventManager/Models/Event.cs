namespace EventManager.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidation.Event;

public class Event
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;
    
    [MaxLength(DescriptionMaxLength)]
    public string? Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int MaxParticipants { get; set; }
    
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
    
    public virtual ICollection<Registration> Registrations { get; set; } 
        = new HashSet<Registration>();
}