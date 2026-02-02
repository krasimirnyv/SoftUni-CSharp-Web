using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Utilities.Validation;

using System.ComponentModel.DataAnnotations;

using Data;

public class ExistenceCheckAttribute : ValidationAttribute
{
    [FromServices] 
    public EventDbContext DbContext { get; set; } = null!;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return new ValidationResult("Value cannot be null.");
        }

        bool categoryExists = DbContext
            .Categories
            .AsNoTracking()
            .Any(c => c.Id.ToString().ToLower() == value.ToString()!.ToLower());
        
        return !categoryExists ? new ValidationResult($"The specified category with ID \"{value}\" does not exist.") : ValidationResult.Success;
    }
}