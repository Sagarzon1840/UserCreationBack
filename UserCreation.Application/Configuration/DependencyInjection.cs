using Microsoft.Extensions.DependencyInjection;
using UserCreation.Application.UseCases.Auth;
using UserCreation.Application.UseCases.Personas;
using UserCreation.Application.UseCases.Usuarios;

namespace UserCreation.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar casos de uso
            services.AddScoped<LoginUseCase>();
            services.AddScoped<CreatePersonaUseCase>();
            services.AddScoped<GetPersonasCreadasUseCase>();
            services.AddScoped<CreateUsuarioUseCase>();
            
            return services;
        }
    }
}
