using FluentValidation;
using Gatherly.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace StudyTaskManager.Application
{
    /// <summary>
    /// Класс для добавления сервисов
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Конструктор для добавления сервисов
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => 
                configuration.RegisterServicesFromAssemblies(assembly));

            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            return services;
        }
    }

}
