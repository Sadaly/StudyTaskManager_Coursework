using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Exceptions.Base;

namespace StudyTaskManager.Domain.Exceptions
{
	public sealed class UserNotFound : NotFoundException
	{
		public UserNotFound(Guid Id) : base($"Пользователь с Id: {Id} не найден.")
		{
		}
		public UserNotFound(User user) : base($"Указанный пользователь (id:{user.Id}, UserName:{user.UserName}) не найден.")
		{
		}
	}
}
