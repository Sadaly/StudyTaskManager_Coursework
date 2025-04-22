using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetByUser;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetByUser
{
    class PersonalChatsGetByUserQueryHandler : ICommandHandler<PersonalChatsGetByUserQuery, List<PersonalChatsGetByUserResponseElements>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatsGetByUserQueryHandler(IUserRepository userRepository, IPersonalChatRepository personalChatRepository)
        {
            _userRepository = userRepository;
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<List<PersonalChatsGetByUserResponseElements>>> Handle(PersonalChatsGetByUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<List<PersonalChatsGetByUserResponseElements>>(user);

            var listPC = await _personalChatRepository.GetChatByUserAsync(user.Value, cancellationToken);
            if (user.IsFailure) return Result.Failure<List<PersonalChatsGetByUserResponseElements>>(listPC);

            var listRes = listPC.Value.Select(pc => new PersonalChatsGetByUserResponseElements(pc, request.UserId)).ToList();

            return listRes;
        }
    }
}
