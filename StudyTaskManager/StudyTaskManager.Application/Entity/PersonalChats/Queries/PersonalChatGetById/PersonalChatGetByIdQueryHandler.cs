using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetById
{
    class PersonalChatGetByIdQueryHandler : IQueryHandler<PersonalChatGetByIdQuery, PersonalChatsResponse>
    {
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatGetByIdQueryHandler(IPersonalChatRepository personalChatRepository)
        {
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<PersonalChatsResponse>> Handle(PersonalChatGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _personalChatRepository.GetByIdAsync(request.IdPersonalChat, cancellationToken);
            if (result.IsFailure) return Result.Failure<PersonalChatsResponse>(result);

            return new PersonalChatsResponse(result.Value);
        }
    }
}
