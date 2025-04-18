using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetById
{
    class PersonalChatGetByIdQueryHandler : IQueryHandler<PersonalChatGetByIdQuery, PersonalChatResponse>
    {
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatGetByIdQueryHandler(IPersonalChatRepository personalChatRepository)
        {
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<PersonalChatResponse>> Handle(PersonalChatGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _personalChatRepository.GetByIdAsync(request.IdPersonalChat, cancellationToken);
            if (result.IsFailure) return Result.Failure<PersonalChatResponse>(result);
            return new PersonalChatResponse(result.Value);
        }
    }
}
