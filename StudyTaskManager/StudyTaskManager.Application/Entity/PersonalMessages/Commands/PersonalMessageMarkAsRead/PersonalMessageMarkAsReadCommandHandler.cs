using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageMarkAsRead
{
    internal class PersonalMessageMarkAsReadCommandHandler : ICommandHandler<PersonalMessageMarkAsReadCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalMessageMarkAsReadCommandHandler(IUnitOfWork unitOfWork, IPresonalMessageRepository presonalMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result> Handle(PersonalMessageMarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _presonalMessageRepository.GetByIdAsync(request.PersonalMessageId, cancellationToken);
            if (message.IsFailure) return message;

            var markAsRea = message.Value.MarkAsRead();
            if (markAsRea.IsFailure) return markAsRea;

            var update = await _presonalMessageRepository.UpdateAsync(message.Value, cancellationToken);
            if (update.IsFailure) return update;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
