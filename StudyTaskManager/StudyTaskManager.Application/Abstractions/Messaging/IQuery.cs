using MediatR;

namespace StudyTaskManager.Application.Abstractions.Messaging
{
	/// <summary>
	/// Запрос на вытягивание
	/// </summary>
	/// <typeparam name="TResponse">Возвращаемый результат запроса</typeparam>
	public interface IQuery<out TResponse> : IRequest<TResponse>
	{
	}
}
