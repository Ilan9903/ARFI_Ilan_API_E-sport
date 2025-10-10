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
                    Name = p.Name,
                    Email = p.Email,
                    RankPlayer = p.RankPlayer,
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
                Name = player.Name,
                Email = player.Email,
                RankPlayer = player.RankPlayer,
                TotalScore = player.TotalScore
            };

            return Ok(playerDto);
        }


        [HttpPost]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            if (await _context.Players.AnyAsync(p => p.Name == createPlayerDto.Name))
            {
                return Conflict("Pseudo already exists.");
            }

            if (await _context.Players.AnyAsync(p => p.Email == createPlayerDto.Email))
            {
                return Conflict("Email already exists.");
            }

            var player = new Models.Player
            {
                Name = createPlayerDto.Name,
                Email = createPlayerDto.Email,
                RankPlayer = createPlayerDto.RankPlayer,
                TotalScore = createPlayerDto.TotalScore,
            };
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            createPlayerDto.Id = player.IdPlayers;
            return CreatedAtAction(nameof(GetPlayer), new { id = createPlayerDto.Id }, createPlayerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, PlayerDto playerDto)
        {
            if (id != playerDto.Id)
            {
                return BadRequest();
            }
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            player.Name = playerDto.Name;
            player.Email = playerDto.Email;
            player.RankPlayer = playerDto.RankPlayer;
            player.TotalScore = playerDto.TotalScore;
            _context.Entry(player).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Players.Any(e => e.IdPlayers == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("leaderboard")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetLeaderboard()
        {
            var topPlayers = await _context.Players
                .OrderByDescending(p => p.TotalScore)
                .Take(10)
                .Select(p => new PlayerDto
                {
                    Id = p.IdPlayers,
                    Name = p.Name,
                    Email = p.Email,
                    RankPlayer = p.RankPlayer,
                    TotalScore = p.TotalScore
                })
                .ToListAsync();

            return Ok(topPlayers);
        }

    }
}
