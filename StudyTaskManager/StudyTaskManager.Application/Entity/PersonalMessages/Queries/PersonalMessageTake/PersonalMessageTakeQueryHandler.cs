using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageTake
{
    public class PersonalMessageTakeQueryHandler : IQueryHandler<PersonalMessageTakeQuery, List<PersonalMessageResponse>>
    {
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalMessageTakeQueryHandler(IPresonalMessageRepository presonalMessageRepository)
        {
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result<List<PersonalMessageResponse>>> Handle(PersonalMessageTakeQuery request, CancellationToken cancellationToken)
        {
            var messages = request.Predicate == null
                ? await _presonalMessageRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _presonalMessageRepository.GetAllAsync(request.StartIndex, request.Count, request.Predicate, cancellationToken);

            if (messages.IsFailure) return Result.Failure<List<PersonalMessageResponse>>(messages);

            return messages.Value.Select(pm => new PersonalMessageResponse(pm)).ToList();
        }
    }
}
