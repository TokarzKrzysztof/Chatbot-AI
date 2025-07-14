using backend.Functions.Command;
using backend.Functions.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ChatController: ControllerBase
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
			var result = await _mediator.Send(data);
			return Ok(result);
		}
	}
}
