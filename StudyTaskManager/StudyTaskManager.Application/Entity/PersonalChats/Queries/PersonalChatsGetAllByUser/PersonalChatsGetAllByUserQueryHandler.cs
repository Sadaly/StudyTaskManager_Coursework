using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetByUser
{
    class PersonalChatsGetAllByUserQueryHandler : ICommandHandler<PersonalChatsGetAllByUserQuery, List<PersonalChatsResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatsGetAllByUserQueryHandler(IUserRepository userRepository, IPersonalChatRepository personalChatRepository)
        {
            _userRepository = userRepository;
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<List<PersonalChatsResponse>>> Handle(PersonalChatsGetAllByUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<List<PersonalChatsResponse>>(user);

            var listPC = await _personalChatRepository.GetChatByUserAsync(user.Value, cancellationToken);
            if (user.IsFailure) return Result.Failure<List<PersonalChatsResponse>>(listPC);

            var listRes = listPC.Value.Select(pc => new PersonalChatsResponse(pc)).ToList();

            return listRes;
        }
    }
}
