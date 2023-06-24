using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TaskContext _context;

        public UserController(TaskContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest(new { Message = "Email already exists" });
            }

            if (user.Password.Length < 6)
            {
                return BadRequest(new { Message = "Password must be of length 6" });

            }

            var newUser = new User
            {
                Email = user.Email,
                FullName = user.FullName,
                Password = user.Password,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(User user)
        {
            var finduser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (finduser == null)
            {
                return NotFound(new { Message = "User not found" });
            }


            if (user.Password != user.Password)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }


            return Ok(new { Message = "Authentication successful" });
        }
    }
}
