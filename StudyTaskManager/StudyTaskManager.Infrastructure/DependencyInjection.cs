using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace StudyTaskManager.Infrustracture
{
    /// <summary>
    /// Класс для добавления сервисов
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Конструктор для добавления сервисов
        /// </summary>
        public static IServiceCollection AddInfrustracture(this IServiceCollection services)
        { 
            return services;
        }
    }
}
