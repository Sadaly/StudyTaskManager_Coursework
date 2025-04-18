using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using System.Runtime.InteropServices;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete
{
    public class SystemRoleDeleteCommandHandler : ICommandHandler<SystemRoleDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISystemRoleRepository _systemRoleRepository;

        public SystemRoleDeleteCommandHandler(IUnitOfWork unitOfWork, ISystemRoleRepository systemRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _systemRoleRepository = systemRoleRepository;
        }

        public async Task<Result> Handle(SystemRoleDeleteCommand request, CancellationToken cancellationToken)
        {
            var remove = await _systemRoleRepository.RemoveAsync(request.Id, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
