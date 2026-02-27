namespace Homies.ViewModels.Event;

/* Data flow is Controller -> View => No Model Validation */
public class SelectEventTypeViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}