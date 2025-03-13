using MediatR;

namespace StudyTaskManager.Application.Abstractions.Messaging
{
	/// <summary>
	/// Обработчик запроса
	/// </summary>
	/// <typeparam name="TQuery">Запрос</typeparam>
	/// <typeparam name="TResponse">Возвращаемое значение</typeparam>
	public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
	where TQuery : IQuery<TResponse>
	{
	}
}
