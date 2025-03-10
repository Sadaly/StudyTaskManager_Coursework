using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace StudyTaskManager.Persistence
{
    /// <summary>
    /// Класс для добавления сервисов
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Конструктор для добавления сервисов
        /// </summary>
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services;
        }
    }

}
