using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
namespace StudyTaskManager.Application.Entity.Users.Queries.TakeUsers
{
    //Todo
    internal class TakeUsersQueryHandler : IQueryHandler<TakeUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public TakeUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserResponse>>> Handle(TakeUsersQuery request, CancellationToken cancellationToken)
        {
            var users = request.Predicate == null
                ? await _userRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _userRepository.GetAllAsync(request.StartIndex, request.Count, request.Predicate, cancellationToken);

            if (users.IsFailure) return Result.Failure<List<UserResponse>>(users.Error);

            var listRes = users.Value.Select(u => new UserResponse(u)).ToList();

            return listRes;
        }
    }
}
