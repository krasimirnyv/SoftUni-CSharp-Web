namespace GarageApp.Common;

public static class EntityValidation
{
    // Class Car
    internal const int CarMakeMinLength = 2;
    internal const int CarMakeMaxLength = 50;
    
    internal const int CarModelMinLength = 1;
    internal const int CarModelMaxLength = 80;
    
    // Class Garage
    internal const int GarageNameMinLength = 3;
    internal const int GarageNameMaxLength = 100;
    
    internal const int GarageLocationMinLength = 2;
    internal const int GarageLocationMaxLength = 150;
}