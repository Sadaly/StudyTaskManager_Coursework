using StudyTaskManager.Application;
using StudyTaskManager.Infrastructure;
using StudyTaskManager.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Gatherly.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StudyTaskManager.Persistence.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using StudyTaskManager.Persistence.Repository;
using Microsoft.Extensions.Options;
using StudyTaskManager.Domain.Entity.User;

namespace ConsoleAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime __timeStart = DateTime.Now; Console.WriteLine($"Тестовый проект просто для проверки реализации.\nНачало работы: {__timeStart}\n------------------------\n");



            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddDbContext<AppDbContext>(                                           // подключение к бд постгрес
                options =>                                                                              //
                {                                                                                       // Изменять нужно в файле appsettings.Development.json
                    options.UseNpgsql("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=dbtest;");    //
                });

            var app = builder.Build();


            Console.WriteLine("args.Length - " + args.Length);
            Console.WriteLine("builder.ToString() - " + builder.ToString());
            Console.WriteLine("builder.Services.ToString() - " + builder.Services.ToString());
            Console.WriteLine("configuration.ToString() - " + configuration.ToString());


            DateTime __timeEnd = DateTime.Now; Console.WriteLine($"\n------------------------\nКонец работы: {__timeEnd}\nВремя работы: {__timeEnd - __timeStart}");
        }
    }
}
