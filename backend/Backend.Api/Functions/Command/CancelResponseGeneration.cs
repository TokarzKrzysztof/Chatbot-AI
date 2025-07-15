using Backend.Database;
using Backend.Infrastructure.SingletonServices;
using Backend.Models.Entities;
using MediatR;

namespace Backend.Api.Functions.Command
{
    public class CancelResponseGenerationCommand : IRequest<Unit>;

    public class Handler : IRequestHandler<CancelResponseGenerationCommand, Unit>
    {
        private readonly BackgroundGeneratorService _generatorService;
        public Handler(BackgroundGeneratorService generatorService)
        {
            _generatorService = generatorService;
        }

        public async Task<Unit> Handle(CancelResponseGenerationCommand request, CancellationToken cancellationToken)
        {
            _generatorService.Cancel();
            return Unit.Value;
        }
    }
}
