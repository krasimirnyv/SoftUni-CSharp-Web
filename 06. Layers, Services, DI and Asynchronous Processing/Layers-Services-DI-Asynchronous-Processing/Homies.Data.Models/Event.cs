namespace Homies.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

using static GCommon.EntityValidations.Event;

public class Event
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)] 
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime Start { get; set; }
    
    [Required]
    public DateTime End { get; set; }

    [Required]
    [ForeignKey(nameof(Organiser))]
    public string OrganiserId { get; set; } = null!;
    
    public virtual IdentityUser Organiser { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Type))]
    public int EventTypeId { get; set; }

    public virtual EventType EventType { get; set; } = null!;
    
    public virtual ICollection<EventParticipant> EventsParticipants { get; set; } 
        = new HashSet<EventParticipant>();
    
    /* Skip navigation property for IdentityUser */
    public virtual ICollection<IdentityUser> Participants { get; set; }
        = new HashSet<IdentityUser>();
}