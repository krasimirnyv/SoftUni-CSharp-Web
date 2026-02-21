namespace GameZone.Common
{
    public static class ValidationConstants
    {
        /* Game Model Validation Constants */
        public const int GameTitleMinLength = 2;
        public const int GameTitleMaxLength = 150;

        public const int GameDescriptionMinLength = 5;
        public const int GameDescriptionMaxLength = 1000;

        public const int GameImageUrlMinLength = 5;
        public const int GameImageUrlMaxLength = 2048;
        
        public const int GamePublisherNameMinLength = 2;
        public const int GamePublisherNameMaxLength = 150;
        
        public const string GameReleasedOnColumnTypeName = "DATE";
        
        
        /* Genre Model Validation Constants */
        public const int GenreNameMinLength = 2;
        public const int GenreNameMaxLength = 100;
        
        /* Application constants */
        public const string DateFormat = "dd.MM.yyyy";
    }
}
