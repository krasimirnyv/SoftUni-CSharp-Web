namespace BookVerse.GCommon.ValidationAttributes;

using System.ComponentModel.DataAnnotations;

using static ValidationConstants;

public class IsbnAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success; // [Required] will catch the null

        if (value is not string raw)
            return new ValidationResult(ErrorMessage ?? "Invalid ISBN value.");

        string isbn = Normalize(raw);

        if (isbn.Length is not (IsbnMin or IsbnMax))
            return new ValidationResult(ErrorMessage ?? $"ISBN must be exactly {IsbnMin} or {IsbnMax} characters (excluding spaces/hyphens).");

        bool isValid = (isbn.Length == IsbnMin || IsValidIsbn10(isbn)) || (isbn.Length == IsbnMax || IsValidIsbn13(isbn));

        return isValid
        ? ValidationResult.Success 
        : new ValidationResult(ErrorMessage ?? $"The ISBN length must be {IsbnMin} or {IsbnMax} characters!");
    }

    private static string Normalize(string input)
        => new string(input
            .Where(c => c != '-' && c != ' ')
            .Select(char.ToUpperInvariant)
            .ToArray());
    
    private static bool IsValidIsbn10(string isbn10)
    {
        for (int i = 0; i < 9; i++) // Format: 9 digits + 1 digit OR 'X'
        {
            if(!char.IsDigit(isbn10[i]))
                return false;
        }
        
        char last = isbn10[^1];
        if (!(char.IsDigit(last) || last == 'X'))
            return false;
        
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            int digit = isbn10[i] - '0';
            sum += (10 - i) * digit;
        }

        int d10 = last == 'X' ? 10 : (last - '0');
        sum += 1 * d10;

        return sum % 11 == 0;
    }
    
    private static bool IsValidIsbn13(string isbn13)
    {
        if (isbn13.Any(c => !char.IsDigit(c)))
            return false;
        
        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            int digit = isbn13[i] - '0';
            sum += (i % 2 == 0) ? digit : 3 * digit;
        }

        int expected = (10 - (sum % 10)) % 10;
        int actual = isbn13[12] - '0';

        return expected == actual;
    }
}