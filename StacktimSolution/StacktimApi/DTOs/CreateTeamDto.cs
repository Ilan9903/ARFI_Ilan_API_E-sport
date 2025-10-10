namespace StacktimApi.DTOs
{
    public class CreateTeamDto
    {
        public string Name { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public int CaptainId { get; set; }
    }
}
