using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Commands.BlockedUserInfoCreate
{
    internal sealed class BlockedUserInfoCreateCommandHandler : ICommandHandler<BlockedUserInfoCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IBlockedUserInfoRepository _blockedUserInfoRepository;

        public BlockedUserInfoCreateCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IBlockedUserInfoRepository blockedUserInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _blockedUserInfoRepository = blockedUserInfoRepository;
        }

        public async Task<Result<Guid>> Handle(BlockedUserInfoCreateCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (userResult.IsFailure) return Result.Failure<Guid>(userResult.Error);

            var isAlreadyThere = await _blockedUserInfoRepository.GetByUser(userResult.Value, cancellationToken);

            if (isAlreadyThere.IsSuccess) return Result.Failure<Guid>(PersistenceErrors.BlockedUserInfo.AlreadyExist);

            var buiResult = BlockedUserInfo.Create(request.Reason, userResult.Value);

            if (buiResult.IsFailure) return Result.Failure<Guid>(buiResult.Error);

            await _blockedUserInfoRepository.AddAsync(buiResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return buiResult.Value.UserId;
        }
    }
}
