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

        [AllowedValues("Bronze", "Silver", "Gold", "Platinium", "Diamond", "Master")]
        public string? Rank_ { get; set; }
    }
}
