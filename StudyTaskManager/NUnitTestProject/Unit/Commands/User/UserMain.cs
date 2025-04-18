using StudyTaskManager.Application.Entity.Users.Commands.UserCreate;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Persistence;
using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Application.Entity.Users.Commands.UserDelete;

namespace NUnitTestProject.Unit.Commands.Users
{
    class UserMain
    {
        class UnitOfWorkStub : IUnitOfWork
        {
            public Task SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }

        readonly IUnitOfWork unitOfWork = new UnitOfWorkStub();
        AppDbContext appDbContext;
        Guid NewUserId;

        [SetUp]
        public void Setup()
        {
            appDbContext = new();
        }
        [TearDown]
        public void Teardown()
        {
            appDbContext.Dispose(); // Закрытие подключения
        }

        [Test, Order(1)]
        public void CreateUser()
        {
            var userCreateCommand = new UserCreateCommand(
                "UserNameTestUniqueName8908201",
                "email8908201@mail",
                "password",
                null,
                null);

            IUserRepository userRepository = new UserRepository(appDbContext);
            ISystemRoleRepository systemRoleRepository = new SystemRoleRepository(appDbContext);

            var handler = new UserCreateCommandHandler(unitOfWork, userRepository, systemRoleRepository);

            // Синхронный вызов асинхронного метода
            var result = handler.Handle(userCreateCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.EqualTo(Guid.Empty));

            NewUserId = result.Value;
        }

        [Test, Order(2)]
        public void DeleteUser()
        {
            //var userDeleteCommand = new UserDeleteCommand(NewUserId);

            //IUserRepository userRepository = new UserRepository(appDbContext);

            //var handler = new DeleteByIdCommandHandler<User>(unitOfWork, userRepository);

            //var result = handler.Handle(userDeleteCommand, CancellationToken.None).GetAwaiter().GetResult();

            //Assert.That(result.Error, Is.EqualTo(Error.None));
            //Assert.That(result.IsSuccess, Is.True);

            //var userExists = userRepository.GetByIdAsync(NewUserId, CancellationToken.None).GetAwaiter().GetResult();
            //Assert.That(userExists.IsFailure, Is.True);
        }
    }
}