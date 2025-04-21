using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupInviteRepository : Generic.IRepository<GroupInvite>
    {
        /// <summary>
        /// Получения приглашения по пользователю(получателю) и группе.
        /// </summary>
        /// <param name="reseiver">Получатель приглашения</param>
        /// <param name="group">Группа, куда приглашают</param>
        Task<Result<GroupInvite>> GetByUserAndGropu(User reseiver, Group group, CancellationToken cancellationToken = default);


        /// <summary>
        /// Получить все приглашения для пользователя.
        /// </summary>
        /// <param name="receiver">Пользователь, которого приглашают в группу.</param>
        Task<Result<List<GroupInvite>>> GetForUserAsync(User receiver, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить часть приглашений для пользователя.
        /// </summary>
        /// <param name="startIndex">Начальный индекс. 1-ый элемент под индексом 0.</param>
        /// <param name="count">Количество взятых значений.</param>
        /// <param name="receiver">Пользователь, которого приглашают.</param>
        /// <returns>Если <paramref name="startIndex"/> или <paramref name="count"/> выходят за рамки БД, ошибки не будет, вернется лишь часть данных, которая находится в рамках списка записей.</returns>
        Task<Result<List<GroupInvite>>> GetForUserAsync(int startIndex, int count, User receiver, CancellationToken cancellationToken = default);


        /// <summary>
        /// Выдать все приглашения от пользователя.
        /// </summary>
        /// <param name="sender">Пользователь, который приглашает.</param>
        Task<Result<List<GroupInvite>>> GetFromUserAsync(User sender, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить часть приглашений от пользователя.
        /// </summary>
        /// <param name="startIndex">Начальный индекс. 1-ый элемент под индексом 0.</param>
        /// <param name="count">Количество взятых значений.</param>
        /// <param name="sender">Пользователь, который приглашает.</param>
        /// <returns>Если <paramref name="startIndex"/> или <paramref name="count"/> выходят за рамки БД, ошибки не будет, вернется лишь часть данных, которая находится в рамках списка записей.</returns>
        Task<Result<List<GroupInvite>>> GetFromUserAsync(int startIndex, int count, User sender, CancellationToken cancellationToken = default);


        /// <summary>
        /// Выдать все приглашения в группе.
        /// </summary>
        Task<Result<List<GroupInvite>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать часть приглашений в группе.
        /// </summary>
        /// <param name="startIndex">Начальный индекс. 1-ый элемент под индексом 0.</param>
        /// <param name="count">Количество взятых значений.</param>
        /// <param name="group"></param>
        /// <returns>Если <paramref name="startIndex"/> или <paramref name="count"/> выходят за рамки БД, ошибки не будет, вернется лишь часть данных, которая находится в рамках списка записей.</returns>
        Task<Result<List<GroupInvite>>> GetByGroupAsync(int startIndex, int count, Group group, CancellationToken cancellationToken = default);
    }
}
