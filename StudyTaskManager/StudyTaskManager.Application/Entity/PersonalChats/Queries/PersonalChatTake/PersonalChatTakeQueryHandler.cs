using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatTake
{
    public class PersonalChatTakeQueryHandler : IQueryHandler<PersonalChatTakeQuery, List<PersonalChatsResponse>>
    {
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatTakeQueryHandler(IPersonalChatRepository personalChatRepository)
        {
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<List<PersonalChatsResponse>>> Handle(PersonalChatTakeQuery request, CancellationToken cancellationToken)
        {
            var chats = request.Perdicate == null
                ? await _personalChatRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _personalChatRepository.GetAllAsync(request.StartIndex, request.Count, request.Perdicate, cancellationToken);

            if (chats.IsFailure) return Result.Failure<List<PersonalChatsResponse>>(chats);

            return chats.Value.Select(pc => new PersonalChatsResponse(pc)).ToList();
        }
    }
}
