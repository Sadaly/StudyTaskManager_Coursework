using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete

{
    public class PersonalMessageDeleteCommandHandler : ICommandHandler<PersonalMessageDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalMessageDeleteCommandHandler(IUnitOfWork unitOfWork, IPresonalMessageRepository presonalMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result> Handle(PersonalMessageDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _presonalMessageRepository.RemoveAsync(request.Id, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
