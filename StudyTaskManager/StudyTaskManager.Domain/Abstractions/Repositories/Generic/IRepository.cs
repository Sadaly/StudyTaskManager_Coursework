using StudyTaskManager.Domain.Shared;
using System.Linq.Expressions;

namespace StudyTaskManager.Domain.Abstractions.Repositories.Generic
{
    /// <summary>
    /// Базовый интерфейс репозиториев для всех сущностей
    /// </summary>
    /// <typeparam name="T">Класс для работы</typeparam>
    public interface IRepository<T> where T : Common.BaseEntity
    {
        #region Добавление изменение удаление реализация
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
        #endregion

        #region GetAllAsync разные вариации
        /// <summary>
        /// Возвращает все объекты.
        /// </summary>
        /// <returns>Возвращает лист, предполагаю что это поменяется когда я возьмусь за реализацию.</returns>
        Task<Result<List<T>>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает часть объектов.
        /// </summary>
        /// <param name="startIndex">Начальный индекс. 1-ый элемент под индексом 0.</param>
        /// <param name="count">Количество взятых значений.</param>
        /// <returns>Список из объектов. Если <paramref name="startIndex"/> или <paramref name="count"/> выходят за рамки БД, ошибки не будет, вернется лишь часть данных, которая находится в рамках списка записей.</returns>
        Task<Result<List<T>>> GetAllAsync(int startIndex, int count, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает все объекты, которые соответствуют предикату.
        /// </summary>
        /// <param name="predicate">Условия для списка.</param>
        /// <returns>Возвращает список, все объекты которого удовлетворяют условию <paramref name="predicate"/>.</returns>
        Task<Result<List<T>>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод возвращающий часть данных из БД по указанному предикату (сначала выполняется условие, а затем берутся элементы).
        /// </summary>
        /// <param name="predicate">Условия для списка.</param>
        /// <param name="startIndex">Начальный индекс. 1-ый элемент под индексом 0.</param>
        /// <param name="count">Количество взятых значений.</param>
        /// <returns>Возвращает часть списка элементы которого удовлетворяют условию <paramref name="predicate"/>. Если <paramref name="startIndex"/> или <paramref name="count"/> выходят за рамки БД, ошибки не будет, вернется лишь часть данных, которая находится в рамках списка записей.</returns>
        public Task<Result<List<T>>> GetAllAsync(
            int startIndex,
            int count,
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);
        #endregion
    }
}