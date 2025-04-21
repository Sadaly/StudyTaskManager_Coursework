using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatDelete
{
    public class PersonalChatDeleteCommandHandler : ICommandHandler<PersonalChatDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonalChatRepository _personalChatRepository;

        public PersonalChatDeleteCommandHandler(IUnitOfWork unitOfWork, IPersonalChatRepository personalChatRepository)
        {
            _unitOfWork = unitOfWork;
            _personalChatRepository = personalChatRepository;
        }


        public async Task<Result> Handle(PersonalChatDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _personalChatRepository.RemoveAsync(request.Id, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
