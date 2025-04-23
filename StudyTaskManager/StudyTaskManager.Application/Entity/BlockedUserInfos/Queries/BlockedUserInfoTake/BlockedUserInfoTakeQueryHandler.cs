using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoTake
{
    internal sealed class BlockedUserInfoTakeQueryHandler : IQueryHandler<BlockedUserInfoTakeQuery, List<BlockedUserInfoResponse>>
    {
        private readonly IBlockedUserInfoRepository _blockedUserInfoRepository;

        public BlockedUserInfoTakeQueryHandler(IBlockedUserInfoRepository blockedUserInfoRepository)
        {
            _blockedUserInfoRepository = blockedUserInfoRepository;
        }

        public async Task<Result<List<BlockedUserInfoResponse>>> Handle(BlockedUserInfoTakeQuery request, CancellationToken cancellationToken)
        {
            var bui = request.Predicate == null
                ? await _blockedUserInfoRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _blockedUserInfoRepository.GetAllAsync(request.StartIndex, request.Count, request.Predicate, cancellationToken);

            if (bui.IsFailure) return Result.Failure<List<BlockedUserInfoResponse>>(bui.Error);

            var listRes = bui.Value.Select(u => new BlockedUserInfoResponse(u)).ToList();

            return listRes;
        }
    }
}
