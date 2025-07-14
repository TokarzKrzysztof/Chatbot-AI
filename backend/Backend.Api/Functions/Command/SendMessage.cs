using Backend.Database;
using Backend.Infrastructure.SingletonServices;
using Backend.Models.Entities;
using MediatR;

namespace backend.Functions.Command
{
	public class SendMessageCommand : IRequest<Unit>
	{
		public string Text { get; set; }
	}

	public class Handler : IRequestHandler<SendMessageCommand, Unit>
	{
		private readonly ApplicationDbContext _context;
		private readonly BackgroundGeneratorService _generatorService;
        public Handler(ApplicationDbContext context, BackgroundGeneratorService generatorService)
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
