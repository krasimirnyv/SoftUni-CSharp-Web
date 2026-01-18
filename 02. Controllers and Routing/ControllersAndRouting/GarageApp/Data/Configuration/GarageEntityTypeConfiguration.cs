namespace GarageApp.Data.Configuration;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GarageEntityTypeConfiguration : IEntityTypeConfiguration<Garage>
{
    private readonly Garage[] garages =
    [
        new Garage
        {
            Id = Guid.Parse("a0556d26-3224-4568-93e4-8ba997308f04"),
            Name = "Sofia Central Garage",
            Location = "Sofia Centre"
        },
        new Garage
        {
            Id = Guid.Parse("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5"),
            Name = "Balkan Star North Garage",
            Location = "Sofia, Balkan Star North"
        },
        new Garage
        {
            Id = Guid.Parse("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c"),
            Name = "Porsche Sofia West Garage",
            Location = "Sofia, Porsche Sofia West"
        }
    ];
    
    public void Configure(EntityTypeBuilder<Garage> entity)
    {
        entity
            .HasData(garages);
    }
}