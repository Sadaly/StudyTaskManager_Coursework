using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetByUser
{
    class PersonalChatGetByUserQueryHandler : ICommandHandler<PersonalChatGetByUserQuery, List<PersonalChat>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatGetByUserQueryHandler(IUserRepository userRepository, IPersonalChatRepository personalChatRepository)
        {
            _userRepository = userRepository;
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<List<PersonalChat>>> Handle(PersonalChatGetByUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<List<PersonalChat>>(user);

            var listPC = await _personalChatRepository.GetChatByUserAsync(user.Value, cancellationToken);
            return listPC;
        }
    }
}
