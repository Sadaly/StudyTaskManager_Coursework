using MediatR;

namespace StudyTaskManager.Application.Abstractions.Messaging
{
	/// <summary>
	/// Обработчик команды
	/// </summary>
	/// <typeparam name="TCommand">Команда на выполнение</typeparam>
	/// <typeparam name="TResponse">Возвращаемое значение</typeparam>
	public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
	{
	}
}
