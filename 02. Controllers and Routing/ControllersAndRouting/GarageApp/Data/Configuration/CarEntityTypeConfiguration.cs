namespace GarageApp.Data.Configuration;

using Models;
using Models.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
{
    private readonly Car[] cars =
    [
        new Car
        {
            Id = Guid.Parse("d542460e-235c-435d-8a6a-30918ce3a8a5"),
            Make = "BMW",
            Model = "320d",
            Year = 2007,
            BodyStyle = CarBodyStyle.Combi,
            IsAvailable = true,
            GarageId = Guid.Parse("a0556d26-3224-4568-93e4-8ba997308f04")
        },
        new Car
        {
            Id = Guid.Parse("aa701bd7-c722-42db-a393-3f1b41f9bdba"),
            Make = "Audi",
            Model = "A4",
            Year = 2018,
            BodyStyle = CarBodyStyle.Sedan,
            IsAvailable = false,
            GarageId = Guid.Parse("a0556d26-3224-4568-93e4-8ba997308f04")
        },
        new Car
        {
            Id = Guid.Parse("dc266079-c3ac-4046-a672-a3c0b0bc6975"),
            Make = "Volkswagen",
            Model = "Golf",
            Year = 2020,
            BodyStyle = CarBodyStyle.Hatchback,
            IsAvailable = true,
            GarageId = Guid.Parse("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5")
        },
        new Car
        {
            Id = Guid.Parse("d4d2dab1-a742-4c8a-bb63-76cc86f016ac"),
            Make = "Toyota",
            Model = "Corolla",
            Year = 2021,
            BodyStyle = CarBodyStyle.Sedan,
            IsAvailable = true,
            GarageId = Guid.Parse("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5")
        },
        new Car
        {
            Id = Guid.Parse("ddb7b8fb-a81d-44e5-a2eb-7d748e090152"),
            Make = "Mercedes-Benz",
            Model = "C220",
            Year = 2017,
            BodyStyle = CarBodyStyle.Sedan,
            IsAvailable = false,
            GarageId = Guid.Parse("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c")
        },
        new Car
        {
            Id = Guid.Parse("9c048e12-c131-4a5e-85af-c0d374bbe04b"),
            Make = "Ford",
            Model = "Focus",
            Year = 2016,
            BodyStyle = CarBodyStyle.Hatchback,
            IsAvailable = true,
            GarageId = Guid.Parse("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c")
        },
        new Car
        {
            Id = Guid.Parse("78ca1339-45e9-4585-8d7e-ea699e6488c2"),
            Make = "Mazda",
            Model = "CX-5",
            Year = 2022,
            BodyStyle = CarBodyStyle.SUV,
            IsAvailable = true,
            GarageId = Guid.Parse("a0556d26-3224-4568-93e4-8ba997308f04")
        },
        new Car
        {
            Id = Guid.Parse("ca9d2135-e400-4684-9eb5-5232c466f428"),
            Make = "Skoda",
            Model = "Octavia",
            Year = 2019,
            BodyStyle = CarBodyStyle.Combi,
            IsAvailable = true,
            GarageId = Guid.Parse("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5")
        },
        new Car
        {
            Id = Guid.Parse("9623f701-ac46-4a9a-9863-0831190da302"),
            Make = "Honda",
            Model = "Civic",
            Year = 2018,
            BodyStyle = CarBodyStyle.Coupe,
            IsAvailable = false,
            GarageId = Guid.Parse("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c")
        },
        new Car
        {
            Id = Guid.Parse("f93dc41c-45a9-40b4-b4b6-bc720ada9c29"),
            Make = "Hyundai",
            Model = "Tucson",
            Year = 2023,
            BodyStyle = CarBodyStyle.SUV,
            IsAvailable = true,
            GarageId = Guid.Parse("a0556d26-3224-4568-93e4-8ba997308f04")
        }
    ];
    public void Configure(EntityTypeBuilder<Car> entity)
    {
        entity
            .HasData(cars);
    }
}