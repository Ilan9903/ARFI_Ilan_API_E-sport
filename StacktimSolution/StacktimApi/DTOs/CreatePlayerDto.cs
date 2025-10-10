using System.ComponentModel.DataAnnotations;

namespace StacktimApi.DTOs
{
    public class CreatePlayerDto
    {
        [Required]
        [StringLength(50)]
        public string Pseudo { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Rank_ { get; set; }
    }
}
