using StudyTaskManager.Domain.Shared;
using System.Data;

namespace StudyTaskManager.Domain.Errors
{
    public static class CommandErrors
    {
        public static readonly Error RoleBelongsToAnotherGroup= new Error(
            "CommandErrors.RoleBelongsToAnotherGroup",
            "Попытка присвоит роль из одной группы участнику из другой");
    }
}
