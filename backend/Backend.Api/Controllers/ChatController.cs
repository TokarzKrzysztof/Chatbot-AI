using backend.Functions.Command;
using backend.Functions.Query;
using Backend.Api.Functions.Command;
using Backend.Infrastructure.SingletonServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly BackgroundGeneratorService _backgroundGeneratorService;
        public ChatController(IMediator mediator, BackgroundGeneratorService backgroundGeneratorService)
        {
            _mediator = mediator;
            _backgroundGeneratorService = backgroundGeneratorService;
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

        [HttpDelete]
        public async Task<IActionResult> CancelResponseGeneration()
        {
            await _mediator.Send(new CancelResponseGenerationCommand());
            return NoContent();
        }

        [HttpGet]
        public async Task GetChatResponse(CancellationToken cancellationToken)
        {
            Response.Headers.ContentType = "text/event-stream";
            await foreach (var current in _backgroundGeneratorService.ListenResponseGeneration(cancellationToken))
            {
                await Response.WriteAsync($"data: {JsonSerializer.Serialize(current)}" + "\n\n");
                await Response.Body.FlushAsync();
            }
        }
    }
}
