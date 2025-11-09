using GeekGarden.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeekGarden.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _http;

        public UsersController(AppDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // Cek HttpContext dan User
            var httpContext = _http.HttpContext;
            if (httpContext == null || httpContext.User == null)
            {
                return Unauthorized(new { message = "User context not found." });
            }

            var userRole = httpContext.User.FindFirst("role")?.Value;
            var userName = httpContext.User.Identity?.Name;

            if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new { message = "Invalid user credentials." });
            }

            IQueryable<Models.User> query = _context.Users;

            if (userRole == "Manager")
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);
                if (currentUser == null)
                {
                    return NotFound(new { message = $"User '{userName}' not found." });
                }

                if (!string.IsNullOrEmpty(currentUser.Department))
                {
                    query = query.Where(u => u.Department == currentUser.Department);
                }
                else
                {
                    return BadRequest(new { message = "User department is not assigned." });
                }
            }
            else if (userRole == "Employee")
            {
                query = query.Where(u => u.Name == userName);
            }

            var users = await query.ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound(new { message = "No users found." });
            }

            return Ok(users);
        }
    }
}
