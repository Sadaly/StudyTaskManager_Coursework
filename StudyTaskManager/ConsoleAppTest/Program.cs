using StudyTaskManager.Application;
using StudyTaskManager.Persistence;
using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;

namespace ConsoleAppTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DateTime __timeStart = DateTime.Now; Console.WriteLine($"Тестовый проект просто для проверки реализации.\nНачало работы: {__timeStart}\n------------------------\n");



            //var builder = WebApplication.CreateBuilder(args);
            //var configuration = builder.Configuration;

            //builder.Services.AddDbContext<AppDbContext>(                                           // подключение к бд постгрес
            //    options =>                                                                              //
            //    {                                                                                       // Изменять нужно в файле appsettings.Development.json
            //        options.UseNpgsql("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=dbtest;");    //
            //    });

            //var app = builder.Build();

            //Console.WriteLine("args.Length - " + args.Length);
            //Console.WriteLine("builder.ToString() - " + builder.ToString());
            //Console.WriteLine("builder.Services.ToString() - " + builder.Services.ToString());
            //Console.WriteLine("configuration.ToString() - " + configuration.ToString());
            User newUser;
            string nameUser;
            using (AppDbContext db = new AppDbContext())
            {
                Console.Write("UserName (<50): ");
                nameUser = Console.ReadLine();
                newUser = User.Create(
                    Guid.Empty,
                    UserName.Create(nameUser).Value,
                    Email.Create("strEmail@mail.com").Value,
                    Password.Create("password").Value,
                    null,
                    null);
                Console.WriteLine($"newUser prev use rep.Add: {newUser.Id} - {newUser.UserName.Value}");
                using (UserRepository userRep = new UserRepository(db))
                {
                    await userRep.AddAsync(newUser);
                }
            }
            Console.WriteLine($"newUser after: {newUser.Id} - {newUser.UserName.Value}");
            using (AppDbContext db = new AppDbContext())
            {
                var users = db.Users.ToList();

                foreach (User u in users)
                {
                    Console.WriteLine($"\t{u.Id} \t- {u.UserName.Value}");
                }
            }



            DateTime __timeEnd = DateTime.Now; Console.WriteLine($"\n------------------------\nКонец работы: {__timeEnd}\nВремя работы: {__timeEnd - __timeStart}\n------------------------\n");
        }
    }
}
