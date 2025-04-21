using StudyTaskManager.Application;
using StudyTaskManager.Persistence;
using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Domain.Entity.User;
using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;

namespace ConsoleAppTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DateTime __timeStart = DateTime.Now; Console.WriteLine($"Тестовый проект просто для проверки реализации.\nНачало работы: {__timeStart}\n------------------------\n");

            using (AppDbContext db = new())
            {
                await Diagnostic(db);

                //Console.WriteLine("--- --- --- --- --- --- --- --- --- --- --- --- ");

                //await Test(db);

                //await Run(db);
            }

            DateTime __timeEnd = DateTime.Now; Console.WriteLine($"\n------------------------\nКонец работы: {__timeEnd}\nВремя работы: {__timeEnd - __timeStart}\n------------------------\n");

            Console.CursorVisible = false;
            //await Task.Delay(500);
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.C) Console.Clear();
            Console.SetCursorPosition(0, 0);
            await Main(args);
        }

        private static async Task Test(AppDbContext db)
        {
            var rep = new UserRepository(db);
            var users = await rep.GetAllAsync(2, 2);

            if (users.IsFailure)
            {
                Console.WriteLine(users.Error.ToString(true));
            }
            else
            {
                foreach (User u in users.Value)
                {
                    Console.WriteLine("* " + u.Id);
                }
            }
        }

        private static async Task Diagnostic(AppDbContext db)
        {
            await PrintListAll.Users(db);
            await PrintListAll.SystemRoles(db);
            await PrintListAll.PersonalChats(db);
            await PrintListAll.PersonatMessages(db);
        }
        private static async Task Run(AppDbContext db)
        {
            (Func<AppDbContext, Task> Function, string Description)[] menuItems =
            [
                (CreateAndAddUser, "Создать и добавить пользователя"),
                (DeleteUser, "Удалить пользователя"),
                (DeletePersonalChat, "Удалить PersonalChat"),
                (ClearAllTables, "Польностью очищает таблицу"),
            ];
            void Print()
            {
                Console.WriteLine("\nВыберите действие:");
                for (int i = 0; i < menuItems.Length; i++)
                {
                    Console.WriteLine($"    {i} - {menuItems[i].Description}");
                }
                Console.WriteLine("    Esc - Выход");
            }
            Print();
            while (true)
            {
                Console.Write("Введите клавишу: ");
                var choice = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine();

                if (choice.Key == ConsoleKey.Escape) break;

                if (char.IsDigit(choice.KeyChar))
                {
                    int selectedIndex = choice.KeyChar - '0';
                    if (selectedIndex >= 0 && selectedIndex < menuItems.Length)
                    {
                        await menuItems[selectedIndex].Function(db);

                        Print();
                        continue;
                    }
                }
                Console.WriteLine("Неверный выбор.");
            }
        }

        #region  work with DB
        private static async Task DeleteUser(AppDbContext db)
        {
            var users = await db.Users.ToListAsync();

            if (users.Count == 0)
            {
                Console.WriteLine("Нет пользователей для удаления.");
                return;
            }
            Console.WriteLine("Выберите пользователя для удаления:");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {users[i].Username.Value}");
            }
            Console.WriteLine("0. Отмена");
            if (!int.TryParse(Console.ReadLine(), out int userChoice) || userChoice < 0 || userChoice > users.Count)
            {
                Console.WriteLine("Неверный выбор.");
                return;
            }
            if (userChoice == 0) return;
            var userToDelete = users[userChoice - 1];
            UserRepository userRep = new UserRepository(db);

            await userRep.RemoveAsync(userToDelete);
            await db.SaveChangesAsync();
            Console.WriteLine("Пользователь успешно удален.");

        }
        private static async Task DeletePersonalChat(AppDbContext db)
        {
            var personalChats = await db.PersonalChats.ToListAsync();

            if (personalChats.Count == 0)
            {
                Console.WriteLine("Нет personalChat для удаления.");
                return;
            }
            Console.WriteLine("Выберите personalChat для удаления:");
            for (int i = 0; i < personalChats.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {personalChats[i].Id}");
            }
            Console.WriteLine("0. Отмена");
            if (!int.TryParse(Console.ReadLine(), out int personalChatChoice) || personalChatChoice < 0 || personalChatChoice > personalChats.Count)
            {
                Console.WriteLine("Неверный выбор.");
                return;
            }
            if (personalChatChoice == 0) return;
            var personalChatToDelete = personalChats[personalChatChoice - 1];
            PersonalCharRepository repository = new(db);

            await repository.RemoveAsync(personalChatToDelete);
            await db.SaveChangesAsync();
            Console.WriteLine("PersonalChat успешно удален.");

        }

        private static async Task CreateAndAddUser(AppDbContext db)
        {
            Console.Write("Username (<50): ");
            string userNameString = Console.ReadLine() ?? "";

            var userName = Username.Create(userNameString);
            if (userName.IsFailure) Console.WriteLine(userName.Error.ToString(true));

            var email = Email.Create($"strEmail_{DateTime.Now:mm:ss}@mail.com");
            if (email.IsFailure) Console.WriteLine(email.Error.ToString(true));

            var password = Password.Create("password");
            if (password.IsFailure) Console.WriteLine(password.Error.ToString(true));

            var newUser = User.Create(
                userName.Value,
                email.Value,
                password.Value,
                null,
                null);
            if (newUser.IsFailure) Console.WriteLine(newUser.Error.ToString(true));

            Console.WriteLine($"newUser.id перед добавлением: ");
            Console.WriteLine($"        {newUser.Value.Id} - {newUser.Value.Username.Value}");

            UserRepository userRep = new UserRepository(db);

            var add = await userRep.AddAsync(newUser.Value);
            if (add.IsFailure) Console.WriteLine(add.Error.ToString(true));

            Console.WriteLine($"после:  {newUser.Value.Id} - {newUser.Value.Username.Value}");
        }

        public static Task ClearAllTables(AppDbContext context)
        {
            // Отключаем проверку внешних ключей для текущей сессии
            context.Database.ExecuteSqlRaw("SET session_replication_role = replica;");

            try
            {
                // Получаем все таблицы из метаданных EF Core
                var tableNames = context.Model.GetEntityTypes()
                    .Select(t => t.GetTableName())
                    .Where(t => t != null)
                    .Distinct()
                    .ToList();

                // Для каждой таблицы выполняем TRUNCATE с CASCADE
                foreach (var tableName in tableNames)
                {
                    context.Database.ExecuteSqlRaw($"TRUNCATE TABLE \"{tableName}\" CASCADE;");
                }
            }
            finally
            {
                // Всегда восстанавливаем проверку внешних ключей
                context.Database.ExecuteSqlRaw("SET session_replication_role = default;");
            }
            return Task.CompletedTask;
        }
        #endregion
    }
}
