using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Application.Abstractions;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.Users.Events
{
    internal sealed class UserRegisteredDomainEventHandler
        : IDomainEventHandler<UserRegisteredDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserRegisteredDomainEventHandler(
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }


        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByIdAsync(
            notification.UserId,
            cancellationToken).Result.Value;

            if (user is null)
            {
                return;
            }

            await _emailService.SendWelcomeEmailAsync(user, cancellationToken);
        }
    }
}
