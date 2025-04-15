using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete
{
    internal class PersonalMessageDeleteCommandHandler : ICommandHandler<PersonalMessageDeleteCommand>
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
            var delete = await _presonalMessageRepository.RemoveAsync(request.PersonalMessageId, cancellationToken);
            if (delete.IsFailure) return delete;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
