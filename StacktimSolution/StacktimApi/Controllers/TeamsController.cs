using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StacktimApi.Data;
using StacktimApi.DTOs;
using System.Collections;

namespace StacktimApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly StacktimDbContext _context;

        public TeamsController(StacktimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            var teams = await _context.Teams
                .Select(t => new TeamDto
                {
                    Id = t.IdTeams,
                    Name = t.Name,
                    Tag = t.Tag,
                    CaptainId = t.CaptainId,
                    CaptainName = t.Captain != null ? t.Captain.Name : null
                })
                .ToListAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            var team = await _context.Teams
                .Where(t => t.IdTeams == id)
                .Select(t => new TeamDto
                {
                    Id = t.IdTeams,
                    Name = t.Name,
                    Tag = t.Tag,
                    CaptainId = t.CaptainId,
                    CaptainName = t.Captain != null ? t.Captain.Name : null
                })
                .FirstOrDefaultAsync();
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeam(CreateTeamDto createTeamDto)
        {
            if (await _context.Teams.AnyAsync(t => t.Name == createTeamDto.Name))
            {
                return BadRequest("Ce nom d'équipe est déjà utilisé.");
            }

            if (await _context.Teams.AnyAsync(t => t.Tag == createTeamDto.Tag))
            {
                return BadRequest("Ce tag est déjà utilisé.");
            }

            var captain = await _context.Players.FindAsync(createTeamDto.CaptainId);
            if (captain == null)
            {
                return BadRequest("L'ID du capitaine n'est pas valide.");
            }

            var team = new Models.Team
            {
                Name = createTeamDto.Name,
                Tag = createTeamDto.Tag,
                CaptainId = createTeamDto.CaptainId,
                CreationDate = DateTime.Now
            };
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            createTeamDto.Id = team.IdTeams;
            return CreatedAtAction(nameof(GetTeam), new { id = team.IdTeams }, createTeamDto);
        }

        [HttpGet("/api/teams/{id}/roster")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetTeamRoster(int id)
        {
            var team = await _context.Teams
                .Include(t => t.TeamPlayers)
                .ThenInclude(tp => tp.Player)
                .FirstOrDefaultAsync(t => t.IdTeams == id);
            if (team == null)
            {
                return NotFound();
            }
            var roster = team.TeamPlayers.Select(tp => new PlayerDto
            {
                Id = tp.Player.IdPlayers,
                Name = tp.Player.Name,
                Email = tp.Player.Email,
                RankPlayer = tp.Player.RankPlayer,
                RegistrationDate = tp.Player.RegistrationDate
            }).ToList();
            return Ok(roster);
        }
    }
}
