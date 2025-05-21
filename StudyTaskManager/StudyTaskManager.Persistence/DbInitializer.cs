using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Persistence;

public static class DbInitializer
{
	public static void Initialize(AppDbContext context)
	{

		if (!context.Users.Any())
		{
			context.Users.AddRange(
				
			);
			context.SaveChanges();
		}
		if (!context.GroupRoles.Any())
		{
			context.GroupRoles.AddRange(
				GroupRole.Create(Title.Create("Создатель").Value, true, true, true, true, true, null).Value,
				GroupRole.Create(Title.Create("Участник").Value, false, false, false, false, false, null).Value
			);
			context.SaveChanges();
        }
        if (!context.GroupTaskStatuses.Any())
        {
            context.GroupTaskStatuses.AddRange(
                GroupTaskStatus.Create(Title.Create("В процессе").Value, true, null, Content.Create("Задача находится в процессе выполнения").Value).Value,
                GroupTaskStatus.Create(Title.Create("Завершена").Value, false, null, Content.Create("Задача завершена и не может быть изменена").Value).Value
            );
            context.SaveChanges();
        }
    }
}