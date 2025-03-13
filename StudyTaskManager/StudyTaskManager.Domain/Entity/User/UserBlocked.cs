using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Заблокированный пользователь (Конкретный класс)
    /// </summary>
    class UserBlocked : AbsUser
    {
        /// <summary>
        /// Информация о блокировании пользователя.
        /// </summary>
        public BlockedUserInfo BlockedUserInfo { get; private set; } = null!;
    }
}
