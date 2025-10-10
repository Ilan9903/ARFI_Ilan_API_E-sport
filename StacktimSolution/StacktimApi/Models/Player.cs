using System;
using System.Collections.Generic;

namespace StacktimApi.Models;

public class Player
{
    public int IdPlayers { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? RankPlayer { get; set; }
    public int TotalScore { get; set; }
    public DateTime RegistrationDate { get; set; }
    public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    public virtual ICollection<Team> Teams { get; set; }
}
