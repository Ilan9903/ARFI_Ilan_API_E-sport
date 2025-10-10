using System;
using System.Collections.Generic;

namespace StacktimApi.Models;

public partial class Team
{
    public int IdTeams { get; set; }

    public string Name { get; set; } = null!;

    public string Tag { get; set; } = null!;

    public int CaptainId { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Player Captain { get; set; } = null!;

    public virtual ICollection<TeamPlayer> TeamPlayers { get; set; } = new List<TeamPlayer>();
}
