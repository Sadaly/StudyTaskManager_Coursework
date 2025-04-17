using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetById
{
    class PersonalChatGetByIdQueryHandler : IQueryHandler<PersonalChatGetByIdQuery, PersonalChat>
    {
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatGetByIdQueryHandler(IPersonalChatRepository personalChatRepository)
        {
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<PersonalChat>> Handle(PersonalChatGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personalChatRepository.GetByIdAsync(request.IdPersonalChat, cancellationToken);
        }
    }
}
