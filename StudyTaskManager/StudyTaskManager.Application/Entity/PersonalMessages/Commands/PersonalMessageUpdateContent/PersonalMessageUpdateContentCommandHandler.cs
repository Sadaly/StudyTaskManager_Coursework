using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;
using System.Linq.Expressions;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageUpdateContent
{
    internal class PersonalMessageUpdateContentCommandHandler : ICommandHandler<PersonalMessageUpdateContentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPresonalMessageRepository _presonalMessageRepository;

        public PersonalMessageUpdateContentCommandHandler(IUnitOfWork unitOfWork, IPresonalMessageRepository presonalMessageRepository)
        {
            _unitOfWork = unitOfWork;
            _presonalMessageRepository = presonalMessageRepository;
        }

        public async Task<Result> Handle(PersonalMessageUpdateContentCommand request, CancellationToken cancellationToken)
        {
            var newContent = Content.Create(request.Content);
            if (newContent.IsFailure) return newContent;

            var message = await _presonalMessageRepository.GetByIdAsync(request.PersonalMessageId, cancellationToken);
            if (message.IsFailure) return message;

            var updateContent = message.Value.UpdateContent(newContent.Value);
            if (updateContent.IsFailure) return updateContent;

            var update = await _presonalMessageRepository.UpdateAsync(message.Value, cancellationToken);
            if (update.IsFailure) return update;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
