using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupChatMessageRepository : Generic.IRepository<GroupChatMessage>
    {
        /// <summary>
        /// Получить сообщение по группе и порядковому номеру.
        /// </summary>
        /// <param name="groupChatId">Идентификатор группового чата, в котором оставлено сообщение.</param>
        /// <param name="ordinal">Порядковый номер сообщения в чате.</param>
        Task<Result<GroupChatMessage>> GetMessageAsync(Guid groupChatId, ulong ordinal, CancellationToken cancellationToken);


        /// <summary>
        /// Получить все сообщения из группового чата.
        /// </summary>
        /// <param name="groupChatId">Идентификатор группового чата, в котором оставлено сообщение.</param>
		Task<Result<List<GroupChatMessage>>> GetMessagesByGroupChatIdAsync(Guid groupChatId, CancellationToken cancellationToken);

        /// <summary>
        /// Получить часть сообщений из группового чата.
        /// </summary>
        /// <param name="startIndex">Начальный индекс. 1-ый элемент под индексом 0.</param>
        /// <param name="count">Количество взятых значений.</param>
        /// <param name="groupChatId">Идентификатор группового чата, в котором оставлено сообщение.</param>
        /// <returns>Если <paramref name="startIndex"/> или <paramref name="count"/> выходят за рамки БД, ошибки не будет, вернется лишь часть данных, которая находится в рамках списка записей.</returns>
        Task<Result<List<GroupChatMessage>>> GetMessagesByGroupChatIdAsync(int startIndex, int count, Guid groupChatId, CancellationToken cancellationToken);


        /// <summary>
        /// Получить все собщения в групповых чатах по Id отправителя.
        /// </summary>
        /// <param name="senderId">Id отправителя сообщения.</param>
        Task<Result<List<GroupChatMessage>>> GetMessagesBySenderIdAsync(Guid senderId, CancellationToken cancellationToken);

        /// <summary>
        /// Получить часть собщений в групповых чатах по Id отправителя.
        /// </summary>
        /// <param name="startIndex">Начальный индекс. 1-ый элемент под индексом 0.</param>
        /// <param name="count">Количество взятых значений.</param>
        /// <param name="senderId">Id отправителя сообщения.</param>
        /// <returns>Если <paramref name="startIndex"/> или <paramref name="count"/> выходят за рамки БД, ошибки не будет, вернется лишь часть данных, которая находится в рамках списка записей.</returns>
        Task<Result<List<GroupChatMessage>>> GetMessagesBySenderIdAsync(int startIndex, int count, Guid senderId, CancellationToken cancellationToken);
    }
}
