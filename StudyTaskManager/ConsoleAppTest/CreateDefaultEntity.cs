using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Persistence;

namespace ConsoleAppTest
{
    class CreateDefaultEntity
    {
        // метод для добавления стандартных ролей группы
        public static async Task AddDefaultGroupRoles(AppDbContext dbContext)
        {
            // Создаем стандартные роли
            var defaultRoles = new List<GroupRole>
            {
                GroupRole.Create(
                    Title.Create("Администратор").Value,
                    canCreateTasks: true,
                    canManageRoles: true,
                    canCreateTaskUpdates: true,
                    canChangeTaskUpdates: true,
                    canInviteUsers: true,
                    group: null).Value,

                GroupRole.Create(
                    Title.Create("Модератор").Value,
                    canCreateTasks: true,
                    canManageRoles: false,
                    canCreateTaskUpdates: true,
                    canChangeTaskUpdates: true,
                    canInviteUsers: true,
                    group: null).Value,

                GroupRole.Create(
                    Title.Create("Участник").Value,
                    canCreateTasks: false,
                    canManageRoles: false,
                    canCreateTaskUpdates: true,
                    canChangeTaskUpdates: false,
                    canInviteUsers: false,
                    group: null).Value,

                GroupRole.Create(
                    Title.Create("Наблюдатель").Value,
                    canCreateTasks: false,
                    canManageRoles: false,
                    canCreateTaskUpdates: false,
                    canChangeTaskUpdates: false,
                    canInviteUsers: false,
                    group: null).Value
            };

            // Добавляем роли в контекст
            await dbContext.Set<GroupRole>().AddRangeAsync(defaultRoles);
            await dbContext.SaveChangesAsync();
        }

        // метод для добавления стандартных статусов задач
        public static async Task AddDefaultTaskStatuses(AppDbContext dbContext)
        {
            // Создаем стандартные статусы
            var defaultStatuses = new List<GroupTaskStatus>
            {
                GroupTaskStatus.Create(
                    Title.Create("Новая").Value,
                    canBeUpdated: true,
                    group: null,
                    description: Content.Create("Задача создана, но еще не взята в работу").Value
                ).Value,

                GroupTaskStatus.Create(
                    Title.Create("В работе").Value,
                    canBeUpdated: true,
                    group: null,
                    description: Content.Create("Задача в процессе выполнения").Value
                ).Value,

                GroupTaskStatus.Create(
                    Title.Create("На проверке").Value,
                    canBeUpdated: true,
                    group: null,
                    description: Content.Create("Задача выполнена и ожидает проверки").Value
                ).Value,

                GroupTaskStatus.Create(
                    Title.Create("Завершена").Value,
                    canBeUpdated: false,
                    group: null,
                    description: Content.Create("Задача успешно завершена").Value
                ).Value,

                GroupTaskStatus.Create(
                    Title.Create("Отложена").Value,
                    canBeUpdated: true,
                    group: null,
                    description: Content.Create("Задача временно отложена").Value
                ).Value,

                GroupTaskStatus.Create(
                    Title.Create("Отменена").Value,
                    canBeUpdated: false,
                    group: null,
                    description: Content.Create("Задача была отменена").Value
                ).Value
            };

            // Добавляем статусы в контекст
            await dbContext.Set<GroupTaskStatus>().AddRangeAsync(defaultStatuses);
            await dbContext.SaveChangesAsync();
        }

        // метод для добавления стандартных системных ролей пользователей
        public static async Task AddDefaultSystemRoles(AppDbContext dbContext)
        {
            // Создаем стандартные системные роли
            var defaultSystemRoles = new List<SystemRole>
            {
                SystemRole.Create(
                    Title.Create("Супер администратор").Value,
                    canViewPeoplesGroups: true,
                    canChangeSystemRoles: true,
                    canBlockUsers: true,
                    canDeleteChats: true
                ).Value,

                SystemRole.Create(
                    Title.Create("Администратор").Value,
                    canViewPeoplesGroups: true,
                    canChangeSystemRoles: false,
                    canBlockUsers: true,
                    canDeleteChats: true
                ).Value,

                SystemRole.Create(
                    Title.Create("Модератор").Value,
                    canViewPeoplesGroups: true,
                    canChangeSystemRoles: false,
                    canBlockUsers: true,
                    canDeleteChats: false
                ).Value,

                SystemRole.Create(
                    Title.Create("Пользователь").Value,
                    canViewPeoplesGroups: false,
                    canChangeSystemRoles: false,
                    canBlockUsers: false,
                    canDeleteChats: false
                ).Value,

                SystemRole.Create(
                    Title.Create("Гость").Value,
                    canViewPeoplesGroups: false,
                    canChangeSystemRoles: false,
                    canBlockUsers: false,
                    canDeleteChats: false
                ).Value
            };

            // Добавляем системные роли в контекст
            await dbContext.Set<SystemRole>().AddRangeAsync(defaultSystemRoles);
            await dbContext.SaveChangesAsync();
        }

        // Общий метод для добавления всех стандартных сущностей
        public static async Task AddAllDefaultEntities(AppDbContext dbContext)
        {
            await AddDefaultGroupRoles(dbContext);
            await AddDefaultTaskStatuses(dbContext);
            await AddDefaultSystemRoles(dbContext);
            await dbContext.SaveChangesAsync();
        }
    }
}