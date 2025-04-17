using StudyTaskManager.Domain.Shared;
using System.Linq.Expressions;

namespace StudyTaskManager.Domain.Abstractions.Repositories.Generic
{
    /// <summary>
    /// Базовый интерфейс репозиториев для всех сущностей
    /// </summary>
    /// <typeparam name="T">Класс для работы</typeparam>
    public interface IRepository<T> : IDisposable where T : Common.BaseEntity
        // IDisposable нужен для закрытия подключения к БД
    {
        /// <summary>
        /// Добавление нового экземпляра. При этом передаваемый экземпляр изменяет свой Id, если Id есть вообще как поле.
        /// </summary>
        /// <param name="entity">Ссылка на entity.</param>
        Task<Result> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет значения на основе переданного экземпляра.
        /// </summary>
        /// <param name="entity">Измененный экземпляр.</param>
        Task<Result> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаление экземпляра.
        /// </summary>
        /// <param name="entity">Ссылка на entity.</param>
        Task<Result> RemoveAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает все объекты.
        /// </summary>
        /// <returns>Возвращает лист, предполагаю что это поменяется когда я возьмусь за реализацию.</returns>
        Task<Result<List<T>>> GetAllAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Возвращает все объекты, которые соответствуют предикату.
		/// </summary>
		/// <returns>Возвращает лист, все объекты которого соответствуют предикату.</returns>
		Task<Result<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

		///// <summary>
		///// Возвращает какое-то кол-во элементов после того как пропустил какое-то кол-во элементов
		///// </summary>
		///// <param name="skip">количество пропущенных элементов</param>
		///// <param name="take">количество элементов, которые он возьмет и вернет</param>
		//// что-то типо пагинации, полезность под вопросом, если нужно сделаю
		//public Task<List<T>> Get(int skip, int take, CancellationToken cancellationToken = default);
	}
}