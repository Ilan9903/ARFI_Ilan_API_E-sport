using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StacktimApi.Data;
using StacktimApi.DTOs;

namespace StacktimApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly StacktimDbContext _context;

        public PlayersController(StacktimDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
        {
            var players = await _context.Players
                .Select(p => new PlayerDto
                {
                    Id = p.IdPlayers,
                    Name = p.Pseudo,
                    Email = p.Email,
                    Rank = p.Rank,
                    TotalScore = p.TotalScore
                })
                .ToListAsync();

            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            var playerDto = new PlayerDto
            {
                Id = player.IdPlayers,
                Name = player.Pseudo,
                Email = player.Email,
                Rank = player.Rank,
                TotalScore = player.TotalScore
            };

            return Ok(playerDto);
        }

        [HttpPost]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(PlayerDto playerDto)
        {
            var player = new Models.Player
            {
                Pseudo = playerDto.Name,
                Email = playerDto.Email,
                Rank = playerDto.Rank,
                TotalScore = playerDto.TotalScore
            };
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            playerDto.Id = player.IdPlayers;
            return CreatedAtAction(nameof(GetPlayer), new { id = playerDto.Id }, playerDto);
        }
    }
}
