namespace StudyTaskManager.Domain.Exceptions.Base
{
	/// <summary>
	/// Ошибка нахождения элемента
	/// </summary>
	public abstract class NotFoundException : Exception
	{
		protected NotFoundException(string message)
			: base(message)
		{
		}
	}
}
