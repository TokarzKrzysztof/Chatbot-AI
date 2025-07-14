using Backend.Database;
using Backend.Database.QueryExtensions;
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
		public Handler(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<MessageDTO>> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
		{
			return await _context.Messages.OrderBy(x => x.CreatedAt).Select(x => x.AsDTO()).ToListAsync();
		}
	}

}
