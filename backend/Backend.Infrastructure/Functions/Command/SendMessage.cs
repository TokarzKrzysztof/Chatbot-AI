using Backend.Database;
using Backend.Infrastructure.SingletonServices;
using Backend.Models.Entities;
using MediatR;

namespace Backend.Infrastructure.Functions.Command
{
	public class SendMessageCommand : IRequest<Unit>
	{
		public string Text { get; set; }
	}

	public class SendMessageHandler : IRequestHandler<SendMessageCommand, Unit>
	{
		private readonly ApplicationDbContext _context;
		private readonly BackgroundGeneratorService _generatorService;
        public SendMessageHandler(ApplicationDbContext context, BackgroundGeneratorService generatorService)
        {
            _context = context;
            _generatorService = generatorService;
        }

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
		{
			_context.Messages.Add(new Message()
			{
				Text = request.Text,
				IsAnswer = false
			});
			await _context.SaveChangesAsync(cancellationToken);

            _ = _generatorService.GenerateAndSaveResponse();

			return Unit.Value;
		}
	}
}
