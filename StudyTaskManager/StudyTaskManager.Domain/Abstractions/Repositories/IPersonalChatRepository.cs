using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IPersonalChatRepository : Generic.IRepositoryWithID<PersonalChat>
    {
        /// <summary>
        /// Возвращает чат с польозвателем. Если чата нет, то создает и возвращает. При этом последовательность передаваемых значений не имеет значения.
        /// Предполагается использовать вместо AddAsync(PersonalChat)
        /// </summary>
        /// <param name="user1">Пользователь, который должен присутствовать в чате.</param>
        /// <param name="user2">Пользователь, который должен присутствовать в чате.</param>
        public Task<PersonalChat> GetChatByUsersAsync(User user1, User user2);

        //public new async Task AddAsync(PersonalChat entity)
        //{
        //    await GetChatByUsersAsync(entity.User1, entity.User2);
        //}
    }
}
