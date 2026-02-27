namespace Homies.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(HelperId), nameof(EventId))]
public class EventParticipant
{
    [Required]
    [ForeignKey(nameof(Helper))]
    public string HelperId { get; set; } = null!;
    
    public virtual IdentityUser Helper { get; set; } = null!;
    
    [Required]
    [ForeignKey(nameof(Event))]
    public Guid EventId { get; set; }

    public virtual Event Event { get; set; } = null!;
}