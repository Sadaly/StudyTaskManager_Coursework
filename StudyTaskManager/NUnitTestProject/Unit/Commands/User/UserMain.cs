using StudyTaskManager.Application.Entity.Users.Commands.UserCreate;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Persistence;
using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Application.Entity.Users.Commands.UserDelete;
using NUnitTestProject.Unit.Commands.SystemRole;

namespace NUnitTestProject.Unit.Commands.Users
{
    class UserMain
    {
        readonly IUnitOfWork unitOfWorkStub = new UnitOfWorkStub();
        AppDbContext appDbContext;
        Guid NewId = Guid.Empty;

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
                "testUser",
                "test@mail",
                "password",
                null,
                null);

            IUserRepository userRepository = new UserRepository(appDbContext);
            ISystemRoleRepository systemRoleRepository = new SystemRoleRepository(appDbContext);

            var handler = new UserCreateCommandHandler(unitOfWorkStub, userRepository, systemRoleRepository);

            // Синхронный вызов асинхронного метода
            var result = handler.Handle(userCreateCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.EqualTo(Guid.Empty));

            NewId = result.Value;
        }

        [Test, Order(2)]
        public void DeleteUser()
        {
            if (NewId == Guid.Empty) throw new Exception("NewId == Guid.Empty");
            var userDeleteCommand = new UserDeleteCommand(NewId);

            IUserRepository userRepository = new UserRepository(appDbContext);

            var handler = new UserDeleteCommandHandler(unitOfWorkStub, userRepository);

            var result = handler.Handle(userDeleteCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.IsSuccess, Is.True);

            var userExists = userRepository.GetByIdAsync(NewId, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(userExists.IsFailure, Is.True);
        }
    }
}