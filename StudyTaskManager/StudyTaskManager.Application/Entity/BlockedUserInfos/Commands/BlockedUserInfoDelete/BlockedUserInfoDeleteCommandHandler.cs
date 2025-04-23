using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Commands.BlockedUserInfoDelete
{
    internal sealed class BlockedUserInfoDeleteCommandHandler : ICommandHandler<BlockedUserInfoDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlockedUserInfoRepository _blockedUserInfoRepository;

        public BlockedUserInfoDeleteCommandHandler(IUnitOfWork unitOfWork, IBlockedUserInfoRepository blockedUserInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _blockedUserInfoRepository = blockedUserInfoRepository;
        }

        public async Task<Result> Handle(BlockedUserInfoDeleteCommand request, CancellationToken cancellationToken)
        {
            var buiresult = await _blockedUserInfoRepository.GetByUser(request.UserId);
            if (buiresult.IsFailure) return Result.Failure(buiresult);

            var delete = await _blockedUserInfoRepository.RemoveAsync(buiresult.Value);
            if (delete.IsFailure) return Result.Failure(delete);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
