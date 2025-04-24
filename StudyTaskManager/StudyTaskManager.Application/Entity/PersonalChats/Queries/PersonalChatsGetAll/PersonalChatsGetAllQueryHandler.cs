using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetAll
{
    class PersonalChatsGetAllQueryHandler : ICommandHandler<PersonalChatsGetAllQuery, List<PersonalChatsResponse>>
    {
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatsGetAllQueryHandler(IPersonalChatRepository personalChatRepository)
        {
            _personalChatRepository = personalChatRepository;
        }

        public async Task<Result<List<PersonalChatsResponse>>> Handle(PersonalChatsGetAllQuery request, CancellationToken cancellationToken)
        {
			var listPC = request.Predicate == null
				? await _personalChatRepository.GetAllAsync(cancellationToken)
				: await _personalChatRepository.GetAllAsync(request.Predicate, cancellationToken);

			if (listPC.IsFailure) return Result.Failure<List<PersonalChatsResponse>>(listPC.Error);

			var listRes = listPC.Value.Select(pc => new PersonalChatsResponse(pc)).ToList();

            return listRes;
        }
    }
}
