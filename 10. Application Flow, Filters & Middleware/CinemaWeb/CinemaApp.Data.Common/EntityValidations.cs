namespace CinemaApp.Data.Common;

public static class EntityValidations
{
    public static class Movie
    {
        public const int TitleMaxLenght = 100;
        
        public const int GenreMaxLenght = 50;

        public const int DirectorMaxLenght = 150;

        public const int DescriptionMaxLenght = 1000;

        public const int ImageUrlMaxLenght = 2048;
    }
}