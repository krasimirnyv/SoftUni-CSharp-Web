namespace CinemaApp.Services.Models.Movie;

using AutoMapper;
using AutoMapping;

using Data.Models;

public class MovieAllDto : IMapFrom<Movie>, IMapTo<Movie>, IHaveCustomMappings
{
    public Guid Id { get; set; }
    
    public string? ImageUrl { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string Director { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public bool IsInUserWatchlist { get; set; }
    
    
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<Movie, MovieAllDto>()
            .ForMember(d => d.IsInUserWatchlist, opt => opt.Ignore());
        
        configuration.CreateMap<MovieAllDto, Movie>()
            .ForMember(d => d.Id, opt => opt.Ignore());
    }
}