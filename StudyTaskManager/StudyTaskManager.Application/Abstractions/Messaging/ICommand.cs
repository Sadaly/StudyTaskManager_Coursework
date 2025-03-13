using MediatR;

namespace StudyTaskManager.Application.Abstractions.Messaging
{
	/// <summary>
	/// Команда на запись
	/// </summary>
	/// <typeparam name="TResponse">Возвращаемое значение</typeparam>
	public interface ICommand<out TResponse> : IRequest<TResponse>
	{
	}
}
