namespace StudyTaskManager.Domain.Exceptions.Base
{
	/// <summary>
	/// Ошибка запроса
	/// </summary>
	public abstract class BadRequestException : Exception
	{
		protected BadRequestException(string message)
			: base(message)
		{
		}
	}
}
