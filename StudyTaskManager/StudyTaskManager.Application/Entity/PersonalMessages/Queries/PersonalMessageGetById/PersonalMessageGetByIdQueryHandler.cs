using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetById
{
    public sealed class PersonalMessageGetByIdQueryHandler : IQueryHandler<PersonalMessageGetByIdQuery, PersonalMessage>
    {
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalMessageGetByIdQueryHandler(IPresonalMessageRepository presonalMessageRepository)
        {
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result<PersonalMessage>> Handle(PersonalMessageGetByIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _presonalMessageRepository.GetByIdAsync(request.PersonalMessageId, cancellationToken);
            if (message.IsFailure) return Result.Failure<PersonalMessage>(message);

            return Result.Success(message.Value);
        }
    }
}
