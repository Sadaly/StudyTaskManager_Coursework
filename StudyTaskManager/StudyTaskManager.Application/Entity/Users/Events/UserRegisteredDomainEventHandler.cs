using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Application.Abstractions;

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


        public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            //Todo
            throw new NotImplementedException();
        }
    }
}
