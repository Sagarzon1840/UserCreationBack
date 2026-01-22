using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserCreation.Application.Ports.Out;
using UserCreation.Infrastructure.Persistence;
using UserCreation.Infrastructure.Repositories;
using UserCreation.Infrastructure.Services;

namespace UserCreation.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Default");

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString, sql =>
                {
                    sql.EnableRetryOnFailure(maxRetryCount: 8);
                });
            });

            // Registrar repositorios
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Registrar servicios
            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
