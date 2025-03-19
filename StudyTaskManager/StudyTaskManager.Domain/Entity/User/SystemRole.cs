using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Системная роль, определяющая права пользователя в системе.
    /// </summary>
    public class SystemRole : BaseEntityWithID
    {
        /// <summary>
        /// Приватный конструктор для создания системной роли.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="name">Название роли.</param>
        /// <param name="canViewPeoplesGroups">Возможность просматривать чужие группы.</param>
        /// <param name="canChangeSystemRoles">Возможность изменять системные роли других пользователей.</param>
        /// <param name="canBlockUsers">Возможность блокировать пользователей.</param>
        /// <param name="canDeleteChats">Возможность удалять чаты.</param>
        private SystemRole(Guid id, Title name, bool canViewPeoplesGroups, bool canChangeSystemRoles, bool canBlockUsers, bool canDeleteChats)
            : base(id)
        {
            Name = name;
            CanViewPeoplesGroups = canViewPeoplesGroups;
            CanChangeSystemRoles = canChangeSystemRoles;
            CanBlockUsers = canBlockUsers;
            CanDeleteChats = canDeleteChats;
        }

        /// <summary>
        /// Название роли.
        /// </summary>
        public Title Name { get; set; } = null!;

        /// <summary>
        /// Возможность просматривать чужие группы.
        /// </summary>
        public bool CanViewPeoplesGroups { get; set; }

        /// <summary>
        /// Возможность изменять системные роли других пользователей.
        /// </summary>
        public bool CanChangeSystemRoles { get; set; }

        /// <summary>
        /// Возможность блокировать пользователей.
        /// </summary>
        public bool CanBlockUsers { get; set; }

        /// <summary>
        /// Возможность удалять чаты.
        /// </summary>
        public bool CanDeleteChats { get; set; }

        /// <summary>
        /// Метод создания новой системной роли.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="name">Название роли.</param>
        /// <param name="canViewPeoplesGroups">Возможность просматривать чужие группы.</param>
        /// <param name="canChangeSystemRoles">Возможность изменять системные роли других пользователей.</param>
        /// <param name="canBlockUsers">Возможность блокировать пользователей.</param>
        /// <param name="canDeleteChats">Возможность удалять чаты.</param>
        /// <returns>Новый экземпляр класса <see cref="SystemRole"/>.</returns>
        public static SystemRole Create(Guid id, Title name, bool canViewPeoplesGroups, bool canChangeSystemRoles, bool canBlockUsers, bool canDeleteChats)
        {
            var systemRole = new SystemRole(id, name, canViewPeoplesGroups, canChangeSystemRoles, canBlockUsers, canDeleteChats);

            // TODO: Добавить создание доменного события о создании системной роли.

            return systemRole;
        }
    }
}
