namespace Homies.GCommon;

public static class EntityValidations
{
    public static class Event
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 20;

        public const int DescriptionMinLength = 15;
        public const int DescriptionMaxLength = 150;
    }
    
    public static class EventType
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 15;
    }
}