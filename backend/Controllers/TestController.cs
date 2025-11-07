using Microsoft.AspNetCore.Mvc;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Test ok" });
        }
    }
}