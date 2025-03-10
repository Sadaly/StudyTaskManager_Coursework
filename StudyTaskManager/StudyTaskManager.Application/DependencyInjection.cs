
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace StudyTaskManager.Application
{
    /// <summary>
    ///  ласс дл€ добавлени€ сервисов
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        /// <summary>
        /// —юда добавл€ютс€ новые сервисы
        /// </summary>
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => 
                configuration.RegisterServicesFromAssemblies(assembly));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }

}
