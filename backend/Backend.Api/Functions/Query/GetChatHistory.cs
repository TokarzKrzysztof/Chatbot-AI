using Backend.Database;
using Backend.Database.QueryExtensions;
using Backend.Infrastructure.SingletonServices;
using Backend.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace backend.Functions.Query
{
    public class GetChatHistoryQuery : IRequest<List<MessageDTO>>;

    public class Handler : IRequestHandler<GetChatHistoryQuery, List<MessageDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly BackgroundGeneratorService _generatorService;
        public Handler(ApplicationDbContext context, BackgroundGeneratorService generatorService)
        {
            _context = context;
            _generatorService = generatorService;
        }

        public async Task<List<MessageDTO>> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Messages.OrderBy(x => x.CreatedAt).Select(x => x.AsDTO()).ToListAsync();

            var pending = _generatorService.GetPendingMessage();
            if (pending != null)
            {
                result.Add(pending.AsDTO());
            }

            return result;
        }
    }

}
