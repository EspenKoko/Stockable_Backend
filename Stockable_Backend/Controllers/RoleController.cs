using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stockable_Backend.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stockable_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("UserRoles")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRoles()
        {
            var userRoles = await _context.UserRoles.ToListAsync();
            return Ok(userRoles);
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }
    }
}
