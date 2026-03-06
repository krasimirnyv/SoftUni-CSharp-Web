namespace CinemaApp.Web.ViewModels.Watchlist;

using System.Globalization;

using Services.AutoMapping;
using CinemaApp.Services.Models.Watchlist;

using AutoMapper;

using static GCommon.ApplicationConstants;

public class WatchlistMovieViewModel : IMapFrom<WatchlistMovieDto>, IHaveCustomMappings
{
    public Guid MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string ReleaseDate { get; set; } = null!;

    public string? ImageUrl { get; set; }
    
    
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<WatchlistMovieDto, WatchlistMovieViewModel>()
            .ForMember(d => d.MovieId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(s => s.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture)));
    }
}