using Backend.Infrastructure.SingletonServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Backend.Infrastructure.StartupExtensions
{
    public static class InfrastructureStartupExtension
    {
        public static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddSingleton<BackgroundGeneratorService>();
        }
    }
}
