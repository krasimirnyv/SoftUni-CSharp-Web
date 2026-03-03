namespace CinemaApp.GCommon;

public static class ViewModelValidation
{
    public static class Movie
    {
        public const int TitleMinLenght = 1;
        public const int TitleMaxLenght = 100;
    
        public const int GenreMinLenght = 3;
        public const int GenreMaxLenght = 50;

        public const int DirectorMinLenght = 10;
        public const int DirectorMaxLenght = 150;

        public const int DescriptionMinLenght = 10;
        public const int DescriptionMaxLenght = 1000;

        public const int ImageUrlMaxLenght = 2048;

        public const int DurationMinLength = 1;
        public const int DurationMaxLength = 300;
    }
    
}