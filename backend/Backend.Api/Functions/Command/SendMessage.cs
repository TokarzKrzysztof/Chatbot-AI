using Backend.Database;
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
		public Handler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
		{
			_context.Messages.Add(new Message()
			{
				Text = request.Text
			});
			await _context.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
