using System.Data;

namespace StudyTaskManager.Domain.Entity
{
    public abstract class SystemRole
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; } = null!;

        /// <summary>
        /// Возможность просматривать чужие группы
        /// </summary>
        public bool Can_View_Peoples_Groups { get; }

        /// <summary>
        /// Возможность изменять системные роли другим
        /// </summary>
        public bool Can_Change_System_Roles { get; }

        /// <summary>
        /// Возможность блокировать пользователя
        /// </summary>
        public bool Can_Block_Users { get; }

        /// <summary>
        /// Возможность удаления чата
        /// </summary>
        public bool Can_Delete_Chats { get; }
    }
}
