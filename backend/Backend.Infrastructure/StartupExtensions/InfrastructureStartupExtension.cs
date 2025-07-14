using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Backend.Infrastructure.SingletonServices;

namespace Backend.Infrastructure.StartupExtensions
{
    public static class InfrastructureStartupExtension
    {
        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<BackgroundGeneratorService>();
        }
    }
}
