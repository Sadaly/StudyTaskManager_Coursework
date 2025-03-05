using System.Data;

namespace StudyTaskManager.Domain.Entity
{
    abstract class SystemRole
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Возможность просматривать чужие группы
        /// </summary>
        public bool The_ability_to_view_other_peoples_groups { get; }

        /// <summary>
        /// Возможность изменять системные роли другим
        /// </summary>
        public bool The_possibility_of_changing_the_system_role_to_others { get; }

        /// <summary>
        /// Возможность блокировать пользователя
        /// </summary>
        public bool The_possibility_of_blocking_the_user { get; }

        /// <summary>
        /// Возможность удаления чата
        /// </summary>
        public bool The_ability_to_delete_a_chat { get; }
    }
}
