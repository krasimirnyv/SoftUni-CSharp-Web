namespace CinemaApp.Web.Infrastructure.Extensions;

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection, Type repositoryType)
    {
        Assembly repositoriesAssembly = repositoryType.Assembly;

        IEnumerable<Type> repositoryInterfaces = repositoriesAssembly
            .GetTypes()
            .Where(t => t.IsInterface &&
                             t.Name.StartsWith("I") && 
                             t.Name.EndsWith("Repository"))
            .ToArray();

        foreach (Type repository in repositoryInterfaces)
        {
            Type implementationType = repositoriesAssembly
                .GetTypes()
                .Single(t => t is { IsClass: true, IsAbstract: false } &&
                                  repository.IsAssignableFrom(t) &&
                                  string.Equals(t.Name, repository.Name.Replace("I", ""), StringComparison.InvariantCultureIgnoreCase));

            serviceCollection.AddScoped(repository, implementationType);
        }
        
        return serviceCollection;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, Type serviceType)
    {
        Assembly servicesAssembly = serviceType.Assembly;

        IEnumerable<Type> servicesInterfaces = servicesAssembly
            .GetTypes()
            .Where(t => t.IsInterface &&
                        t.Name.StartsWith("I") && 
                        t.Name.EndsWith("Service"))
            .ToArray();

        foreach (Type service in servicesInterfaces)
        {
            Type implementationType = servicesAssembly
                .GetTypes()
                .Single(t => t is { IsClass: true, IsAbstract: false } &&
                             service.IsAssignableFrom(t) &&
                             string.Equals(t.Name, service.Name.Replace("I", ""), StringComparison.InvariantCultureIgnoreCase));

            serviceCollection.AddScoped(service, implementationType);
        }
        
        return serviceCollection;
    }
}