using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleCreate;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdatePrivileges;
using StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdateTitle;
using StudyTaskManager.Application.Entity.Users.Commands.UserCreate;
using StudyTaskManager.Application.Entity.Users.Commands.UserDelete;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Persistence;
using StudyTaskManager.Persistence.Repository;

namespace NUnitTestProject.Unit.Commands.SystemRole
{
    class SystemRoleMain
    {
        class UnitOfWorkStub : IUnitOfWork
        {
            public Task SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }

        readonly IUnitOfWork unitOfWorkStub = new UnitOfWorkStub();

        AppDbContext appDbContext;
        SystemRoleRepository repository;
        Guid NewId = Guid.Empty;

        [SetUp]
        public void Setup()
        {
            appDbContext = new();
            repository = new SystemRoleRepository(appDbContext);
        }
        [TearDown]
        public void Teardown()
        {
            appDbContext.Dispose(); // Закрытие подключения
        }



        [Test, Order(1)]
        public void Create()
        {
            string name = "_testUniqueName";
            var command = new SystemRoleCreateCommand(
                name,
                false,
                false,
                false,
                false);

            var handler = new SystemRoleCreateCommandHandler(unitOfWorkStub, repository);

            var result = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.Value, Is.Not.EqualTo(Guid.Empty));

            NewId = result.Value;

            var role = repository.GetByIdAsync(NewId, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(role.Error, Is.EqualTo(Error.None));

            Assert.That(role.Value.Name.Value, Is.EqualTo(name));
            Assert.That(role.Value.CanViewPeoplesGroups, Is.False);
            Assert.That(role.Value.CanChangeSystemRoles, Is.False);
            Assert.That(role.Value.CanBlockUsers, Is.False);
            Assert.That(role.Value.CanDeleteChats, Is.False);
        }

        [Test, Order(2)]
        public void UpdatePrivileges()
        {
            if (NewId == Guid.Empty) throw new Exception("NewId == Guid.Empty");
            var command = new SystemRoleUpdatePrivilegesCommand(
                NewId,
                true,
                true,
                true,
                true);

            var handler = new SystemRoleUpdatePrivilegesCommandHandler(unitOfWorkStub, repository);

            var result = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(result.Error, Is.EqualTo(Error.None));

            var role = repository.GetByIdAsync(NewId, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(role.Error, Is.EqualTo(Error.None));

            Assert.That(role.Value.CanViewPeoplesGroups, Is.True);
            Assert.That(role.Value.CanChangeSystemRoles, Is.True);
            Assert.That(role.Value.CanBlockUsers, Is.True);
            Assert.That(role.Value.CanDeleteChats, Is.True);
        }

        [Test, Order(3)]
        public void UpdateTitle()
        {
            if (NewId == Guid.Empty) throw new Exception("NewId == Guid.Empty");

            string newName = "_testNewTitleUnique";
            var command = new SystemRoleUpdateTitleCommand(NewId, newName);

            var handler = new SystemRoleUpdateTitleCommandHandler(unitOfWorkStub, repository);

            var result = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(result.Error, Is.EqualTo(Error.None));

            var role = repository.GetByIdAsync(NewId, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(role.Error, Is.EqualTo(Error.None));

            Assert.That(role.Value.Name.Value, Is.EqualTo(newName));
        }

        [Test, Order(4)]
        public void Delete()
        {
            if (NewId == Guid.Empty) throw new Exception("NewId == Guid.Empty");
            var command = new SystemRoleDeleteCommand(NewId);

            var handler = new SystemRoleDeleteCommandHandler(unitOfWorkStub, repository);

            var result = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(result.Error, Is.EqualTo(Error.None));

            var role = repository.GetByIdAsync(NewId, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(role.IsFailure, Is.True);
        }
    }
}
