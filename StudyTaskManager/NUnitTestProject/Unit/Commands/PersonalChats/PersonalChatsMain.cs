using NUnitTestProject.Unit.Commands.SystemRole;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Persistence.Repository;
using StudyTaskManager.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Application.Entity.Users.Commands.UserCreate;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatCreate;
using StudyTaskManager.Application.Entity.Users.Commands.UserDelete;
using static StudyTaskManager.Domain.Errors.PersistenceErrors;
using StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatDelete;
using StudyTaskManager.Application.Entity.PersonalChats.Commands.PersonalChatAddMessage;
using StudyTaskManager.Application.Entity.PersonalMessages.Commands.PersonalMessageDelete;

namespace NUnitTestProject.Unit.Commands.PersonalChats
{
    class PersonalChatsMain
    {
        readonly IUnitOfWork unitOfWorkStub = new UnitOfWorkStub();

        AppDbContext appDbContext;

        PersonalCharRepository chatRepository;
        PersonalMessageRepository messageRepository;
        UserRepository userRepository;
        SystemRoleRepository systemRoleRepository;

        Guid NewIdUser1 = Guid.Empty;
        Guid NewIdUser2 = Guid.Empty;
        Guid NewIdChat = Guid.Empty;
        Guid NewIdMessage = Guid.Empty;

        [SetUp]
        public void Setup()
        {
            appDbContext = new();
            chatRepository = new PersonalCharRepository(appDbContext);
            messageRepository = new PersonalMessageRepository(appDbContext);
            userRepository = new UserRepository(appDbContext);
            systemRoleRepository = new SystemRoleRepository(appDbContext);
        }
        [TearDown]
        public void Teardown()
        {
            appDbContext.Dispose(); // Закрытие подключения
        }



        [Test, Order(1)]
        public void Create()
        {
            var user1CreateCommand = new UserCreateCommand(
                "testPersonalChatUser1",
                "testPersonalChatUser1@mail",
                "password",
                null,
                null);
            var user2CreateCommand = new UserCreateCommand(
                "testPersonalChatUser2",
                "testPersonalChatUser2@mail",
                "password",
                null,
                null);

            var userCreateCommandHandler = new UserCreateCommandHandler(unitOfWorkStub, userRepository, systemRoleRepository);

            var resultUserCreate = userCreateCommandHandler.Handle(user1CreateCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(resultUserCreate.Error, Is.EqualTo(Error.None));
            Assert.That(resultUserCreate.IsSuccess, Is.True);
            Assert.That(resultUserCreate.Value, Is.Not.EqualTo(Guid.Empty));

            NewIdUser1 = resultUserCreate.Value;

            resultUserCreate = userCreateCommandHandler.Handle(user2CreateCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(resultUserCreate.Error, Is.EqualTo(Error.None));
            Assert.That(resultUserCreate.IsSuccess, Is.True);
            Assert.That(resultUserCreate.Value, Is.Not.EqualTo(Guid.Empty));

            NewIdUser2 = resultUserCreate.Value;

            var user1 = userRepository.GetByIdAsync(NewIdUser1).GetAwaiter().GetResult();
            var user2 = userRepository.GetByIdAsync(NewIdUser2).GetAwaiter().GetResult();

            var chatCreateCommand = new PersonalChatCreateCommand(NewIdUser1, NewIdUser2);

            var chatCreateCommandHandler = new PersonalChatCreateCommandHandler(unitOfWorkStub, userRepository, chatRepository);

            var resultChatCreate = chatCreateCommandHandler.Handle(chatCreateCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(resultChatCreate.Error, Is.EqualTo(Error.None));
            Assert.That(resultChatCreate.IsSuccess, Is.True);
            Assert.That(resultChatCreate.Value, Is.Not.EqualTo(Guid.Empty));

            NewIdChat = resultChatCreate.Value;

            var chatEntity = chatRepository.GetChatByUsersAsync(user1.Value, user2.Value, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(chatEntity.Error, Is.EqualTo(Error.None));
            Assert.That(chatEntity.IsSuccess, Is.True);

        }
        [Test, Order(2)]
        public void AddMessage()
        {
            if (NewIdChat == Guid.Empty) throw new Exception($"{nameof(NewIdChat)} == Guid.Empty");

            string messStr = "test message.";

            var commandAdd = new PersonalChatAddMessageCommand(NewIdUser1, NewIdUser2, messStr);

            var handler = new PersonalChatAddMessageCommandHandler(unitOfWorkStub, userRepository, chatRepository, messageRepository);


            var result = handler.Handle(commandAdd, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.IsSuccess, Is.True);

            var entity = messageRepository.GetByIdAsync(result.Value).GetAwaiter().GetResult();


            Assert.That(entity.Error, Is.EqualTo(Error.None));
            Assert.That(entity.IsSuccess, Is.True);
            Assert.That(entity.Value.Id, Is.EqualTo(result.Value));
            Assert.That(entity.Value.Content.Value, Is.EqualTo(messStr));

            NewIdMessage = result.Value;
        }

        [Test, Order(3)]
        public void Delete()
        {
            if (NewIdChat == Guid.Empty) throw new Exception($"{nameof(NewIdChat)} == Guid.Empty");
            if (NewIdMessage == Guid.Empty) throw new Exception($"{nameof(NewIdMessage)} == Guid.Empty");


            var messageDeleteCommand = new PersonalMessageDeleteCommand(NewIdMessage);

            var messageHandler = new PersonalMessageDeleteCommandHandler(unitOfWorkStub, messageRepository);

            var messageRresult = messageHandler.Handle(messageDeleteCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(messageRresult.Error, Is.EqualTo(Error.None));
            Assert.That(messageRresult.IsSuccess, Is.True);


            var chatDeleteCommand = new PersonalChatDeleteCommand(NewIdChat);

            var chatHandler = new PersonalChatDeleteCommandHandler(unitOfWorkStub, chatRepository);

            var chatResult = chatHandler.Handle(chatDeleteCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(chatResult.Error, Is.EqualTo(Error.None));
            Assert.That(chatResult.IsSuccess, Is.True);


            DeleteUser(NewIdUser1);
            DeleteUser(NewIdUser2);
        }

        private void DeleteUser(Guid idUser)
        {
            if (idUser == Guid.Empty) throw new Exception($"{nameof(idUser)} == Guid.Empty");
            var userDeleteCommand = new UserDeleteCommand(idUser);

            var handler = new UserDeleteCommandHandler(unitOfWorkStub, userRepository);

            var result = handler.Handle(userDeleteCommand, CancellationToken.None).GetAwaiter().GetResult();

            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.IsSuccess, Is.True);

            var userExists = userRepository.GetByIdAsync(idUser, CancellationToken.None).GetAwaiter().GetResult();
            Assert.That(userExists.IsFailure, Is.True);
        }
    }
}
