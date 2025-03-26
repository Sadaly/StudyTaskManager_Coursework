using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Системная роль, определяющая права пользователя в системе.
    /// </summary>
    public class SystemRole : BaseEntityWithID
    {
        private SystemRole(Guid id) : base(id) { }
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
            : this(id)
        {
            Name = name;
            CanViewPeoplesGroups = canViewPeoplesGroups;
            CanChangeSystemRoles = canChangeSystemRoles;
            CanBlockUsers = canBlockUsers;
            CanDeleteChats = canDeleteChats;
        }

        #region свойства

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

        #endregion

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
        public static Result<SystemRole> Create(Guid id, Title name, bool canViewPeoplesGroups, bool canChangeSystemRoles, bool canBlockUsers, bool canDeleteChats)
        {
            var systemRole = new SystemRole(id, name, canViewPeoplesGroups, canChangeSystemRoles, canBlockUsers, canDeleteChats);

			systemRole.RaiseDomainEvent(new SystemRoleCreatedDomainEvent(systemRole.Id));

			return Result.Success(systemRole);
        }
		public Result UpdateTitle(Title Title)
		{
			this.Name = Title;

			this.RaiseDomainEvent(new SystemRoleNameUpdatedDomainEvent(this.Id));

			return Result.Success();
		}

		public Result UpdatePrivileges(bool canViewPeoplesGroups, bool canChangeSystemRoles, bool canBlockUsers, bool canDeleteChats)
        {
            CanViewPeoplesGroups = canViewPeoplesGroups;
            CanChangeSystemRoles = canChangeSystemRoles;
            CanBlockUsers = canBlockUsers;
            CanDeleteChats = canDeleteChats;

            this.RaiseDomainEvent(new SystemRolePrivilegesUpdatedDomainEvent(this.Id));

			return Result.Success();
        }
	}
}
