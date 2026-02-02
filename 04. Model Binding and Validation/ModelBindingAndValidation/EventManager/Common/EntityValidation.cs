namespace EventManager.Common;

public static class EntityValidation
{
    public static class Event
    {
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 100;
        
        public const int DescriptionMinLength = 2;
        public const int DescriptionMaxLength = 1000;
        
        public const int MinParticipantsValue = 1;
        public const int MaxParticipantsValue = 500;
    }
    
    public static class Category
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 100;
    }

    public static class Registration
    {
        public const int ParticipantNameMinLength = 2;
        public const int ParticipantNameMaxLength = 100;
        
        public const int EmailMinLength = 5;
        public const int EmailMaxLength = 100;
    }
}