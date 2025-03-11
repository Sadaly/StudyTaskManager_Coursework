using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Заблокированный пользователь (Конкректный класс)
    /// </summary>
    class UserBloked : AbsUser
    {
        /// <summary>
        /// Информация о блокированни пользователя.
        /// </summary>
        public BlockedUserInfo BlockedUserInfo { get; private set; } = null!;
    }
}
