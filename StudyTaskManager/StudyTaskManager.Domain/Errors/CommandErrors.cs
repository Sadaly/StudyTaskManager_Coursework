using StudyTaskManager.Domain.Shared;
using System.Data;

namespace StudyTaskManager.Domain.Errors
{
    public static class CommandErrors
    {
        public static readonly Error RoleBelongsToAnotherGroup = new(
            "CommandErrors.RoleBelongsToAnotherGroup",
            "Попытка присвоит роль из одной группы участнику из другой");

        public static readonly Error DeleteSharedRole = new(
            "CommandErrors.DeleteSharedRole",
            "Попытка удаления общей роли");

        public static readonly Error DeleteFromAnotherGroup = new(
            "CommandErrors.DeleteFromAnotherGroup",
            "Попытка удаления из другой группы");

        public static readonly Error UserBlocked = new(
            "CommandErrors.UserBlocked",
            "Пользователь заблокирован");
    }
}
