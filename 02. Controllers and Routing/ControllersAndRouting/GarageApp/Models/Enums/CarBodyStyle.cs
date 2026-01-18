namespace GarageApp.Models.Enums;

public enum CarBodyStyle
{
    Unknown = 0,

    // Passenger cars
    Sedan = 1,
    Hatchback = 2,
    Coupe = 3,
    Convertible = 4,
    Combi = 5,
    Liftback = 6,
    Fastback = 7,

    // SUVs & crossovers
    SUV = 8,
    Crossover = 9,

    // Vans & MPVs
    Minivan = 10,
    Van = 11,
    Microvan = 12,

    // Utility
    Pickup = 13,
    ChassisCab = 14,

    // Sports & special
    Roadster = 15,
    Targa = 16,
    ShootingBrake = 17,

    // Luxury & niche
    Limousine = 18,
    Landaulet = 19,

    // Off-road & special
    OffRoad = 20,
    Buggy = 21,
    
    Other = 100
}