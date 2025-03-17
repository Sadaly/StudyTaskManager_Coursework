using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    public abstract class SystemRole : BaseEntityWithID
    {
        /// <summary>
        /// Название
        /// </summary>
        public SystemRoleName Name { get; set; } = null!;

        /// <summary>
        /// Возможность просматривать чужие группы
        /// </summary>
        public bool Can_View_Peoples_Groups { get; set; }

        /// <summary>
        /// Возможность изменять системные роли другим
        /// </summary>
        public bool Can_Change_System_Roles { get; set; }

        /// <summary>
        /// Возможность блокировать пользователя
        /// </summary>
        public bool Can_Block_Users { get; set; }

        /// <summary>
        /// Возможность удаления чата
        /// </summary>
        public bool Can_Delete_Chats { get; set; }
    }
}
