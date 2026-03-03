namespace CinemaApp.Web.ViewModels.Movie;

using System.ComponentModel.DataAnnotations;

using static GCommon.ViewModelValidation.Movie;
using static GCommon.OutputMessages.Movie;

public class MovieFormModel
{
    [Required(ErrorMessage = TitleRequiredMessage)]
    [MinLength(TitleMinLenght, ErrorMessage = TitleMinLengthMessage)]
    [MaxLength(TitleMaxLenght, ErrorMessage = TitleMaxLengthMessage)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = GenreRequiredMessage)]
    [MinLength(GenreMinLenght, ErrorMessage = GenreMinLengthMessage)]
    [MaxLength(GenreMaxLenght, ErrorMessage = GenreMaxLengthMessage)]
    public string Genre { get; set; } = null!;

    [Required(ErrorMessage = DirectorRequiredMessage)]
    [MinLength(DirectorMinLenght, ErrorMessage = DirectorNameMinLengthMessage)]
    [MaxLength(DirectorMaxLenght, ErrorMessage = DirectorNameMaxLengthMessage)]
    public string Director { get; set; } = null!;

    [Required(ErrorMessage = DurationRequiredMessage)]
    [Range(DurationMinLength, DurationMaxLength, ErrorMessage = DurationRangeMessage)]
    public int Duration { get; set; }

    [Required(ErrorMessage = ReleaseDateRequiredMessage)]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = DescriptionRequiredMessage)]
    [MinLength(DescriptionMinLenght, ErrorMessage = DescriptionMinLengthMessage)]
    [MaxLength(DescriptionMaxLenght, ErrorMessage = DescriptionMaxLengthMessage)]
    public string Description { get; set; } = null!;

    [Url]
    [MaxLength(ImageUrlMaxLenght, ErrorMessage = ImageUrlMaxLengthMessage)]
    public string? ImageUrl { get; set; }
}