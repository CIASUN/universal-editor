using Microsoft.AspNetCore.Mvc;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "pong" });
        }
    }
}