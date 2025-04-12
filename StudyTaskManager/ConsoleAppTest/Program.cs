using StudyTaskManager.Application;
using StudyTaskManager.Persistence;
using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DateTime __timeStart = DateTime.Now; Console.WriteLine($"Тестовый проект просто для проверки реализации.\nНачало работы: {__timeStart}\n------------------------\n");

            await PrintListAll.Users();

            await Run();

            DateTime __timeEnd = DateTime.Now; Console.WriteLine($"\n------------------------\nКонец работы: {__timeEnd}\nВремя работы: {__timeEnd - __timeStart}\n------------------------\n");
        }
        private static async Task Run()
        {
            var actions = new Dictionary<string, Func<Task>>
            {
                ["1"] = CreateAndAddUser,
                ["2"] = PrintListAll.Users,
                ["3"] = DeleteUser,
            };

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Создать и добавить пользователя");
                Console.WriteLine("2 - Вывести список всех пользователей");
                Console.WriteLine("3 - Удалить пользователя");
                Console.WriteLine("0 - Выход");

                var choice = Console.ReadLine();

                if (choice == "0") break;

                if (actions.TryGetValue(choice, out var selectedAction))
                {
                    await selectedAction();
                }
                else
                {
                    Console.WriteLine("Неверный выбор");
                }
            }
        }

        private static async Task DeleteUser()
        {
            using (AppDbContext db = new AppDbContext())
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
                using (UserRepository userRep = new UserRepository(db))
                {
                    await userRep.RemoveAsync(userToDelete);
                    await db.SaveChangesAsync();
                    Console.WriteLine("Пользователь успешно удален.");
                }
            }
        }

        private static async Task CreateAndAddUser()
        {
            User newUser;
            string nameUser;
            using (AppDbContext db = new AppDbContext())
            {
                Console.Write("Username (<50): ");
                nameUser = Console.ReadLine() ?? "";
                newUser = User.Create(
                    Username.Create(nameUser).Value,
                    Email.Create("strEmail@mail.com").Value,
                    Password.Create("password").Value,
                    null,
                    null).Value;
                Console.WriteLine($"newUser перед добавлением: {newUser.Id} - {newUser.Username.Value}");
                using (UserRepository userRep = new UserRepository(db))
                {
                    await userRep.AddAsync(newUser);
                    await db.SaveChangesAsync();
                }
            }
            Console.WriteLine($"newUser после: {newUser.Id} - {newUser.Username.Value}");
        }
    }
}
