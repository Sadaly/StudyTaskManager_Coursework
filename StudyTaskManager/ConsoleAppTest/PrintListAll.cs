using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Persistence;

namespace ConsoleAppTest
{
    public static class PrintListAll
    {
        public static async Task Users()
        {
            using var db = new AppDbContext();
            using var userRepository = new UserRepository(db);
            var users = await userRepository.GetAllAsync();
            if (users.IsFailure)
                throw new Exception(users.Error.Code + " - " + users.Error.Message);

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
                columnNames: new[] {
                    "ID",
                    "Имя",
                    "Email",
                    "Телефон",
                    "ID системной роли",
                    "Дата регистрации"
                },
                rows: tableData
            );
        }

        private static string FormatWithVerificationStatus(string? value, bool isVerified)
        {
            if (value == null)
                return "null";

            return $"{value} ({(isVerified ? "+" : "-")})";
        }
    }
}