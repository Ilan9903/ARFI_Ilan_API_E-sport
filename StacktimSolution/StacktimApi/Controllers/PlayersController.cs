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
    }
}
