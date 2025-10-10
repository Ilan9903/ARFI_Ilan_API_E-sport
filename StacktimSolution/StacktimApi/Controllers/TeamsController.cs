using Microsoft.AspNetCore.Mvc;
using StacktimApi.Data;
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
        public async Task<ActionResult<IEnumerable<TeamDto>>>
    }
}
