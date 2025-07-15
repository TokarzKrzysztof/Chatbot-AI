using Backend.Database;
using Backend.Infrastructure.SingletonServices;
using Backend.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Functions.Command
{
    public class SetMessageReactionCommand : IRequest<Unit>
    {
        public Guid MessageId { get; set; }
        public MessageReaction Reaction { get; set; }
    }

    public class SetMessageReactionHandler : IRequestHandler<SetMessageReactionCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        public SetMessageReactionHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SetMessageReactionCommand request, CancellationToken cancellationToken)
        {
            await _context.Messages
                .Where(x => x.Id == request.MessageId && x.IsAnswer).ExecuteUpdateAsync(s => s.SetProperty(x => x.Reaction, request.Reaction));
            return Unit.Value;
        }
    }
}
