using StudyTaskManager.Domain.Entity.Group;
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
	}
}