using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    public class SystemRole : BaseEntityWithID
    {
        private SystemRole(Guid id, Title Name, bool Can_View_Peoples_Groups, bool Can_Change_System_Roles, bool Can_Block_Users, bool Can_Delete_Chats) : base(id)
        {
            this.Name = Name;
            this.Can_View_Peoples_Groups = Can_View_Peoples_Groups;
            this.Can_Change_System_Roles = Can_Change_System_Roles;
            this.Can_Block_Users = Can_Block_Users;
            this.Can_Delete_Chats = Can_Delete_Chats;
        }

        /// <summary>
        /// Название
        /// </summary>
        public Title Name { get; set; } = null!;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Уникальный Id</param>
        /// <param name="Name">Название</param>
        /// <param name="Can_View_Peoples_Groups">Возможность просматривать чужие группы</param>
        /// <param name="Can_Change_System_Roles">Возможность изменять системные роли другим</param>
        /// <param name="Can_Block_Users">Возможность блокировать пользователя</param>
        /// <param name="Can_Delete_Chats">Возможность удаления чата</param>
        /// <returns>Новый экземпляр класс <see cref="SystemRole"/></returns>
        public SystemRole Create(Guid id, Title Name, bool Can_View_Peoples_Groups, bool Can_Change_System_Roles, bool Can_Block_Users, bool Can_Delete_Chats)
        {
            var SystemRole = new SystemRole(id, Name, Can_View_Peoples_Groups, Can_Change_System_Roles, Can_Block_Users, Can_Delete_Chats);
            
            //Todo создание события

            return SystemRole;
        }
    }
}
