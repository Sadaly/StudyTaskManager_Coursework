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
        public Task AddAsync(T entity);

        /// <summary>
        /// Обновляет значения на основе переданного экземпляра.
        /// </summary>
        /// <param name="entity">Измененный экземпляр.</param>
        public Task UpdateAsync(T entity);

        /// <summary>
        /// Удаление экземпляра.
        /// </summary>
        /// <param name="entity">Ссылка на entity.</param>
        public Task RemoveAsync(T entity);

        /// <summary>
        /// Возвращает все объеты.
        /// </summary>
        /// <returns>Возвращает лист, предплагаю что это поменяется когда я возьмусь за реализацию.</returns>
        public Task<List<T>> GetAllAsync();

        ///// <summary>
        ///// Возвращает какоето кол-во элементов после того как пропустил какоето кол-во элементов
        ///// </summary>
        ///// <param name="skip">количество пропущенных элементов</param>
        ///// <param name="take">количество элементов, которые он возьмет и вернет</param>
        //// что-то типо пагинации, полезность под вопросом, если нужно сделаю
        //public Task<List<T>> Get(int skip, int take);
    }
}