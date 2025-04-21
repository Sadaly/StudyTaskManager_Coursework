using NUnitTestProject.Unit.Commands.SystemRole;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Persistence;
using StudyTaskManager.Application.Entity.Groups.Commands.GroupCreate;
using StudyTaskManager.Domain.Abstractions.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StudyTaskManager.Application.Abstractions.Messaging;
using Azure;
using MediatR;
using StudyTaskManager.Domain.Shared;

namespace NUnitTestProject.Unit.Commands.Group
{
    class GroupMain
    {
        /*
                readonly IUnitOfWork unitOfWorkStub = new UnitOfWorkStub();

                AppDbContext appDbContext;

                PersonalCharRepository chatRepository;
                PersonalMessageRepository messageRepository;
                UserRepository userRepository;
                SystemRoleRepository systemRoleRepository;
                GroupRoleRepository groupRoleRepository;
                GroupRepository groupRepository;

                private Guid defaultRoleId;

                [SetUp]
                public void Setup()
                {
                    appDbContext = new();
                    chatRepository = new(appDbContext);
                    messageRepository = new(appDbContext);
                    userRepository = new(appDbContext);
                    systemRoleRepository = new(appDbContext);
                    groupRoleRepository = new(appDbContext);
                    groupRepository = new(appDbContext);

                }
                [TearDown]
                public void Teardown()
                {
                    appDbContext.Dispose(); // Закрытие подключения
                }



                [Test, Order(1)]
                public void Create()
                {
                    var result = Func.Processing(
                        new GroupCreateCommand("Title test group", "description", defaultRoleId),
                        new GroupCreateCommandHandler(unitOfWorkStub, groupRepository, groupRoleRepository));


                    Assert.That(result.Error, Is.EqualTo(Error.None));
                    Assert.That(result.IsSuccess, Is.True);
                    Assert.That(result.Value, Is.Not.EqualTo(Guid.Empty));


                }

                [Test, Order(2)]
                public void CreateRole() { }

                [Test, Order(3)]
                public void SendInvite() { }

                [Test, Order(4)]
                public void RemoveInvite() { }

                [Test, Order(5)]
                public void RemoveUser() { }

                [Test, Order(6)]
                public void RemoveRole() { }
                [Test, Order(7)]
                public void Delete() { }


        */
    }
}
