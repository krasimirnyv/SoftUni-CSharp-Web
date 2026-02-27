namespace Homies.Data.Models;

using System.ComponentModel.DataAnnotations;

using static GCommon.EntityValidations.EventType;

public class EventType
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; }
        = new List<Event>();
}