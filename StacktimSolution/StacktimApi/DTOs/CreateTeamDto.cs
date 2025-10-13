using System.ComponentModel.DataAnnotations;

namespace StacktimApi.DTOs
{
    public class CreateTeamDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength =3)]
        [RegularExpression("^[A-Z]{3}$", ErrorMessage = "Le tag ne doit contenir que des MAJ")]
        public string Tag { get; set; } = null!;

        [Required]
        public int CaptainId { get; set; }
    }
}
