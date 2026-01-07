using Microsoft.AspNetCore.Mvc;

namespace sonarqube_test.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeatherForecastController : ControllerBase
	{
		[HttpGet("bad")]
		public IActionResult Bad()
		{
			string password = "123456"; // security issue
			int x = 0;
			int y = 10 / x;             // runtime bug
			return Ok(password);
		}
	}

}
