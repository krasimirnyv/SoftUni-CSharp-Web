namespace CinemaApp.Services.AutoMapping;

using AutoMapper;

public interface IHaveCustomMappings
{
    void CreateMappings(IProfileExpression configuration);
}