using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetAll
{
    public class PersonalMessageGetAllQueryHandler : IQueryHandler<PersonalMessageGetAllQuery, List<PersonalMessageResponse>>
    {
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalMessageGetAllQueryHandler(IPresonalMessageRepository presonalMessageRepository)
        {
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result<List<PersonalMessageResponse>>> Handle(PersonalMessageGetAllQuery request, CancellationToken cancellationToken)
        {
            var messages = request.Predicate == null
                ? await _presonalMessageRepository.GetAllAsync(cancellationToken)
                : await _presonalMessageRepository.GetAllAsync(request.Predicate, cancellationToken);

            if (messages.IsFailure) return Result.Failure<List<PersonalMessageResponse>>(messages);

            return messages.Value.Select(pm => new PersonalMessageResponse(pm)).ToList();
        }
    }
}
