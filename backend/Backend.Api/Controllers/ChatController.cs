using backend.Functions.Command;
using backend.Functions.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Backend.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ChatController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ChatController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetChatHistory()
		{
			var result = await _mediator.Send(new GetChatHistoryQuery());
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand data)
		{
			await _mediator.Send(data);
			return NoContent();
		}

		[HttpGet]
		public async Task GetChatResponse([FromQuery] Guid messageId)
		{
			Response.Headers.ContentType = "text/event-stream";

			for (var i = 0; i < 100; i++)
			{
				var result = "1";

				await Response.WriteAsync($"data: {result}" + "\n\n");
				await Response.Body.FlushAsync();

				await Task.Delay(100); // Simulate processing time
			}
		}
	}
}
