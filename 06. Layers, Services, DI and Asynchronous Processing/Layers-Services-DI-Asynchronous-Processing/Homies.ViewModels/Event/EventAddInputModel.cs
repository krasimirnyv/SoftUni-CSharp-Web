namespace Homies.ViewModels.Event;

using System.ComponentModel.DataAnnotations;

using static GCommon.EntityValidations.Event;

/* Data flow is Views/Form (User) -> Controller => Model Validation is required */
public class EventAddInputModel
{
    /* Model Input Start */
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;
    
    [Required]
    public DateTime Start { get; set; }
    
    [Required]
    public DateTime End { get; set; }

    [Required]
    public int EventTypeId { get; set; }
    
    /* Model Input End */
    // Model Output -> Nested ViewModel
    public IEnumerable<SelectEventTypeViewModel> EventTypes { get; set; }
        = new List<SelectEventTypeViewModel>();
}