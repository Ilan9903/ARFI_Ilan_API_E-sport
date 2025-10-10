using System.ComponentModel.DataAnnotations;

namespace StacktimApi.DTOs
{
    public class UpdatePlayerDto
    {
        [StringLength(50)]
        public string? Pseudo { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public string? Rank_ { get; set; } 
    }
}
