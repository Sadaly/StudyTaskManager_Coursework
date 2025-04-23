using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Persistence;
using StudyTaskManager.Domain.Abstractions.Repositories;

namespace ConsoleAppTest
{
    public static class PrintListAll
    {
        public static async Task Users(AppDbContext db)
        {
            string FormatWithVerificationStatus(string? value, bool isVerified)
            {
                if (value == null)
                    return "null";

                return $"{value} ({(isVerified ? "+" : "-")})";
            }
            var userRepository = new UserRepository(db);
            var users = await userRepository.GetAllAsync();
            if (users.IsFailure) throw new Exception(users.Error.Code + " - " + users.Error.Message);

            var tableData = users.Value.Select(user => new string[]
            {
                user.Id.ToString(),
                user.Username?.Value ?? "null",
                FormatWithVerificationStatus(user.Email?.Value, user.IsEmailVerifed),
                FormatWithVerificationStatus(user.PhoneNumber?.Value, user.IsPhoneNumberVerifed),
                user.SystemRoleId?.ToString() ?? "null",
                user.RegistrationDate.ToString("dd.MM.yyyy HH:mm:ss")
            }).ToList();

            TablePrinter.PrintTable(
                tableTitle: "Список пользователей",
                columnNames: [
                    "ID",
                    "Имя",
                    "Email",
                    "Телефон",
                    "ID системной роли",
                    "Дата регистрации"
                ],
                rows: tableData
            );
        }

        public static async Task SystemRoles(AppDbContext db)
        {
            var systemRoleRepository = new SystemRoleRepository(db);
            var systemRoles = await systemRoleRepository.GetAllAsync();
            if (systemRoles.IsFailure) throw new Exception(systemRoles.Error.Code + " - " + systemRoles.Error.Message);

            var tableData = systemRoles.Value.Select(role => new string[]
            {
                role.Id.ToString(),
                role.Name?.Value ?? "null",
                role.CanViewPeoplesGroups ? "+" : "-",
                role.CanChangeSystemRoles ? "+" : "-",
                role.CanBlockUsers ? "+" : "-",
                role.CanDeleteChats ? "+" : "-"
            }).ToList();

            TablePrinter.PrintTable(
                tableTitle: "Список системных ролей",
                columnNames: [
                    "ID",
                    "Название",
                    "Просмотр чужих групп",
                    "Изменение ролей",
                    "Блокировка пользователей",
                    "Удаление чатов"
                ],
                rows: tableData
            );
        }

        public static async Task PersonalChats(AppDbContext db)
        {
            var systemRoleRepository = new PersonalCharRepository(db);
            var list = await systemRoleRepository.GetAllAsync();
            if (list.IsFailure) throw new Exception(list.Error.Code + " - " + list.Error.Message);

            var tableData = list.Value.Select(entity => new string[]
            {
                entity.Id.ToString(),
                entity.User1Id.ToString(),
                entity.User2Id.ToString(),
            }).ToList();

            TablePrinter.PrintTable(
                tableTitle: "Список PersonalChats",
                columnNames: [
                    "ID",
                    "User1Id",
                    "User2Id"
                ],
                rows: tableData
            );
        }

        public static async Task PersonatMessages(AppDbContext db)
        {
            var repository = new PersonalMessageRepository(db);
            var list = await repository.GetAllAsync();
            if (list.IsFailure) throw new Exception(list.Error.Code + " - " + list.Error.Message);

            var tableData = list.Value.Select(entity => new string[]
            {
                entity.Id.ToString(),
                entity.PersonalChatId.ToString(),
                entity.SenderId.ToString(),
                entity.Content.Value,
                entity.DateWriten.ToString("dd.MM.yyyy HH:mm:ss")
            }).ToList();

            TablePrinter.PrintTable(
                tableTitle: "Список PersonatMessages",
                columnNames: [
                    "ID",
                    "PersonalChatId",
                    "SenderId",
                    "Content",
                    "DateWriten",
                ],
                rows: tableData
            );
        }

        public static async Task GroupRoles(AppDbContext db)
        {
            var groupRoleRepository = new GroupRoleRepository(db);
            var groupRoles = await groupRoleRepository.GetAllAsync();
            if (groupRoles.IsFailure) throw new Exception(groupRoles.Error.Code + " - " + groupRoles.Error.Message);

            var tableData = groupRoles.Value.Select(role => new string[]
            {
                role.Id.ToString(),
                role.Title?.Value ?? "null",
                role.GroupId?.ToString() ?? "Common role",
                role.CanCreateTasks ? "+" : "-",
                role.CanManageRoles ? "+" : "-",
                role.CanCreateTaskUpdates ? "+" : "-",
                role.CanChangeTaskUpdates ? "+" : "-",
                role.CanInviteUsers ? "+" : "-"
            }).ToList();

            TablePrinter.PrintTable(
                tableTitle: "Список ролей групп",
                columnNames: [
                    "ID",
                    "Название",
                    "ID группы",
                    "Создание задач",
                    "Управление ролями",
                    "Создание обновлений",
                    "Изменение обновлений",
                    "Приглашение пользователей"
                ],
                rows: tableData
            );
        }

        public static async Task GroupTaskStatuses(AppDbContext db)
        {
            var groupTaskStatusRepository = new GroupTaskStatusRepository(db);
            var groupTaskStatuses = await groupTaskStatusRepository.GetAllAsync();
            if (groupTaskStatuses.IsFailure) throw new Exception(groupTaskStatuses.Error.Code + " - " + groupTaskStatuses.Error.Message);

            var tableData = groupTaskStatuses.Value.Select(status => new string[]
            {
                status.Id.ToString(),
                status.Name?.Value ?? "null",
                status.GroupId?.ToString() ?? "Common status",
                status.CanBeUpdated ? "+" : "-",
                status.Description?.Value ?? "null"
            }).ToList();

            TablePrinter.PrintTable(
                tableTitle: "Список статусов задач групп",
                columnNames: [
                    "ID",
                    "Название",
                    "ID группы",
                    "Можно обновлять",
                    "Описание"
                ],
                rows: tableData
            );
        }
    }
}