using System;
using System.Collections.Generic;

namespace StacktimApi.Models;

public partial class Player
{
    public int IdPlayers { get; set; }

    public string Pseudo { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Rank { get; set; }

    public int TotalScore { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
