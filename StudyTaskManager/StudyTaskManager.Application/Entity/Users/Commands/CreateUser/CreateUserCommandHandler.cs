using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Todo: использовать конструктор для класса
            var user = new User{
                UserName = request.UserName,
                Email = request.Email,
                NumberPhone = request.NumberPhone,
                SystemRoleId = request.SystemRoleId,
                PasswordHash = request.Password,
            };
            
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;

        }
    }
}
