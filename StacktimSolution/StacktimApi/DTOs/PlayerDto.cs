namespace StacktimApi.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }
        public string? RankPlayer { get; set; }
        public int TotalScore { get; set; }
    }
}
