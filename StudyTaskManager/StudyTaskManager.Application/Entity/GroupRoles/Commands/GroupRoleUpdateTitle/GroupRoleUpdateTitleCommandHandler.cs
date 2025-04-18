using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;
using System.Runtime.InteropServices;

namespace StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleUpdateTitle
{
    public class GroupRoleUpdateTitleCommandHandler : ICommandHandler<GroupRoleUpdateTitleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupRoleUpdateTitleCommandHandler(IUnitOfWork unitOfWork, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result> Handle(GroupRoleUpdateTitleCommand request, CancellationToken cancellationToken)
        {
            var title = Title.Create(request.Title);
            if (title.IsFailure) return title;

            var role = await _groupRoleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (role.IsFailure) return role;

            var updateTitle = role.Value.UpdateTitle(title.Value);
            if (updateTitle.IsFailure) return updateTitle;

            var updateDB = await _groupRoleRepository.UpdateAsync(role.Value, cancellationToken);
            if (updateDB.IsFailure) return updateDB;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
