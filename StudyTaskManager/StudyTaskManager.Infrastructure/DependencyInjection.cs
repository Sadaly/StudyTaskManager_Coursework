
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace StudyTaskManager.Infrustracture
{
    /// <summary>
    ///  ласс дл€ добавлени€ сервисов
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustracture(this IServiceCollection services)
        /// <summary>
        /// —юда добавл€ютс€ новые сервисы
        /// </summary>
        { 
            return services;
        }
    }
}
