using Backend.Database;
using Backend.Database.QueryExtensions;
using Backend.Infrastructure.SingletonServices;
using Backend.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Backend.Api.Functions.Query
{
    public class GetChatHistoryQuery : IRequest<List<MessageDTO>>;

    public class GetChatHistoryHandler : IRequestHandler<GetChatHistoryQuery, List<MessageDTO>>
    {
        private readonly ApplicationDbContext _context;
        private readonly BackgroundGeneratorService _generatorService;
        public GetChatHistoryHandler(ApplicationDbContext context, BackgroundGeneratorService generatorService)
        {
            _context = context;
            _generatorService = generatorService;
        }

        public async Task<List<MessageDTO>> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
        {
            // workaround for cancel on page reload, to make sure that pending response is already stored in DB
            while (_generatorService.HasPendingMessage())
            {
                await Task.Delay(50);
            }

            return await _context.Messages.OrderBy(x => x.CreatedAt).Select(x => x.AsDTO()).ToListAsync();
        }
    }

}
