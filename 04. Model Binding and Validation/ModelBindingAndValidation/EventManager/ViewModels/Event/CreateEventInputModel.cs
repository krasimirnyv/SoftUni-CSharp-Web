using EventManager.Utilities.Validation;

namespace EventManager.ViewModels.Event;

using System.ComponentModel.DataAnnotations;

using Models;

using static Common.EntityValidation.Event;

public class CreateEventInputModel : IValidatableObject
{
    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;
    
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string? Description { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Range(MinParticipantsValue, MaxParticipantsValue)]
    public int MaxParticipants { get; set; }

    [Required]
    [ExistenceCheck]
    public int CategoryId { get; set; }
    
    public IEnumerable<Category>? Categories { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate >= EndDate)
        {
            yield return new ValidationResult(
                "End date must be after the start date.",
                [nameof(EndDate)]);
        }
    }
}