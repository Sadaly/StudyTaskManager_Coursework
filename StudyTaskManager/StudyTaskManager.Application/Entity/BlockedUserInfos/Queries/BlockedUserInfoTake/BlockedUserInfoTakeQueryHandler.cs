using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoTake
{
    internal sealed class BlockedUserInfoTakeQueryHandler : IQueryHandler<BlockedUserInfoTakeQuery, BlockedUserInfoListResponse>
    {
        private readonly IBlockedUserInfoRepository _blockedUserInfoRepository;

        public BlockedUserInfoTakeQueryHandler(IBlockedUserInfoRepository blockedUserInfoRepository)
        {
            _blockedUserInfoRepository = blockedUserInfoRepository;
        }

        public async Task<Result<BlockedUserInfoListResponse>> Handle(BlockedUserInfoTakeQuery request, CancellationToken cancellationToken)
        {

            Result<List<BlockedUserInfo>> buiResult;
            if (request.predicate == null)
                buiResult = await _blockedUserInfoRepository.TakeAsync(request.StartIndex, request.Count, null, cancellationToken);
            else
                buiResult = await _blockedUserInfoRepository.TakeAsync(request.StartIndex, request.Count, request.predicate, cancellationToken);

            if (buiResult.IsFailure) return Result.Failure<BlockedUserInfoListResponse>(buiResult.Error);

            return new BlockedUserInfoListResponse(buiResult.Value);
        }
    }
}
