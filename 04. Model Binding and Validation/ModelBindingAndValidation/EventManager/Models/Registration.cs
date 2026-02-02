namespace EventManager.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidation.Registration;

public class Registration
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    [MaxLength(ParticipantNameMaxLength)]
    public string ParticipantName { get; set; } = null!;
    
    [Required]
    [MaxLength(EmailMaxLength)]
    public string Email { get; set; } = null!;
    
    [ForeignKey(nameof(Event))]
    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;
}