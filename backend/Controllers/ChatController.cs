using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class ChatController: ControllerBase
	{
		public async Task<IActionResult> GetChatHistory()
		{
			return Ok();
		}
	}
}
