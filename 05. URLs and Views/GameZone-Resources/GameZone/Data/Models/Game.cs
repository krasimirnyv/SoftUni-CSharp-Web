namespace GameZone.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using static Common.ValidationConstants;

    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GameDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MaxLength(GameImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(GamePublisherNameMaxLength)]
        public string PublisherName { get; set; } = null!;

        [Required]
        [Column(TypeName = GameReleasedOnColumnTypeName)]
        public DateOnly ReleasedOn { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        [ForeignKey(nameof(GenreId))]
        public virtual Genre Genre { get; set; } = null!;
    }
}
