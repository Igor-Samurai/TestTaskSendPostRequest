using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using TestPostJson.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestPostJson.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class TestPostController: ControllerBase
    {
        private readonly AppConfig _config;

        public TestPostController(IOptions<AppConfig> config)
        {
            _config = config.Value;
        }

        [HttpPost("testPost")]
        public IActionResult testPost([FromBody] object rawJson)
        {
            string jsonString = JsonSerializer.Serialize(rawJson);
            Task.Delay(_config.Timeout).Wait();
            return Ok(new { message = "Принято", rawJson });
        }
    }
}
