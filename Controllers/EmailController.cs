using GeekGarden.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekGarden.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromQuery] string to, [FromQuery] string subject, [FromQuery] string message)
        {
            var result = await _emailService.SendEmailAsync(to, subject, message);
            if (result)
                return Ok(new { success = true, message = "Email sent successfully" });
            else
                return BadRequest(new { success = false, message = "Failed to send email" });
        }
    }
}
