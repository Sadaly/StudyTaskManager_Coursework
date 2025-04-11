using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Errors
{
    public static class PersistenceErrors
    {
        public static readonly Error UnknowError = new(
            "UnknowError",
            "Неизвестная ошибка");

        public static class BlockedUserInfo
        {
            public static readonly Error AlreadyExist = new(
                "BlockedUserInfo.AlreadyExist",
                "Запись о блокировку уже существует");
            public static readonly Error NotFound = new(
                "BlockedUserInfo.NotFound",
                "Информация о заблокированном пользователе не найдена");
        }

        public static class GroupChatMessage
        {
            public static readonly Error NotFound = new(
                "GroupChatMessage.NotFound",
                "Сообщение в групповом чате не найдено");
            public static readonly Error IdEmpty = new(
                "GroupChatMessage.IdEmpty",
                "Сообщение в групповом чате не имеет уникальный идентификатор");
            public static readonly Error AlreadyExist = new(
                "GroupChatMessage.AlreadyExist",
                "Сообщение в групповом чате уже существует");
        }

        public static class GroupChatParticipantLastRead
        {
            public static readonly Error NotFound = new(
                "GroupChatParticipantLastRead.NotFound",
                "Запись о последнем прочитанном сообщении пользователя в групповом чате не найдена");
            public static readonly Error AlreadyExist = new(
                "GroupChatParticipantLastRead.AlreadyExist",
                "Запись о прочитанном сообщении в групповом чате уже существует");
        }

        public static class GroupChatParticipant
        {
            public static readonly Error NotFound = new(
                "GroupChatParticipant.NotFound",
                "Запись о нахождении пользователя в групповом чате не найдена");
            public static readonly Error UserDoesNotBelongToTheGroupChat = new(
                "GroupChatParticipant.UserDoesNotBelongToTheGroupChat",
                "Пользователь не принадлежит групповому чату");
            public static readonly Error AddingToAPublicChat = new(
                "GroupChatParticipant.AddingToAPublicChat",
                "Попытка добавления к публичному чату");
            public static readonly Error AlreadyExist = new(
                "GroupChatParticipant.AlreadyExist",
                "Пользователь уже в групповом чате");
        }

        public static class GroupChat
        {
            public static readonly Error NotFound = new(
                "GroupChat.NotFound",
                "Группа не найдена");
            public static readonly Error NotUniqueName = new(
                "GroupChat.NotUniqueName",
                "Название группового чата не уникально");
            public static readonly Error IdEmpty = new(
                "GroupChat.IdEmpty",
                "Групповой чат не имеет Id");
            public static readonly Error AlreadyExist = new(
                "GroupChat.AlreadyExist",
                "Этот чат в группе уже существует");
        }

        public static class GroupInvite
        {
            public static readonly Error Accepted = new(
                "GroupInvite.Accepted",
                "Приглашение уже было принято. И не может быть принято или отклонено");
            public static readonly Error Declined = new(
                "GroupInvite.Declined",
                "Приглашение уже было отклонено. И не может быть принято или отклонено");
            public static readonly Error UserIsAlreadyInTheGroup = new(
                "GroupInvite.UserIsAlreadyInTheGroup",
                "Пользователь уже в чате");
            public static readonly Error UserAlreadyHasAnInvitationToThisGroup = new(
                "GroupInvite.UserAlreadyHasAnInvitationToThisGroup",
                "Пользователь уже имеет приглашение в эту группу");
            public static readonly Error NotFound = new(
                "GroupInvite.NotFound",
                "Запись о приглашении в группу не найдена");
            public static readonly Error AlreadyExist = new(
                "GroupInvite.AlreadyExist",
                "Запись о приглашении уже существует");
        }

        public static class GroupRole
        {
            public static readonly Error NotFound = new(
                "Group.NotFound",
                "Роль не найдена");
            public static readonly Error NotUniqueName = new(
                "GroupRole.NotUniqueName",
                "Название групповой роли не уникально");
            public static readonly Error IdEmpty = new(
                "GroupRole.IdEmpty",
                "Групповая роль не имеет Id");
            public static readonly Error AlreadyExist = new(
                "GroupRole.AlreadyExist",
                "Запись о роли в группе уже существует");
        }

        public static class GroupTaskStatus
        {
            public static readonly Error NotFound = new(
                "GroupTaskStatus.NotFound",
                "Статус задачи не найден");
            public static readonly Error CantBeUpdated = new(
                "GroupTaskStatus.CantBeUpdated",
                "Статус задачи не позволяет создавать новые апдейты");
            public static readonly Error IdEmpty = new(
                "GroupTaskStatus.IdEmpty",
                "Групповой статус задачи не имеет Id");
            public static readonly Error AlreadyExists = new(
                "GroupTaskStatus.AlreadyExists",
                "Статус задачи уже существует");
            public static readonly Error NotUniqueName = new(
                "GroupTaskStatus.NotUniqueName",
                "Название статуса задачи не уникально");
        }

        public static class GroupTaskUpdate
        {
            public static readonly Error NotFound = new(
                "GroupTaskUpdate.NotFound",
                "Апдейт задачи не найден");
            public static readonly Error IdEmpty = new(
                "GroupTaskUpdate.IdEmpty",
                "Апдейт задачи не имеет Id");
            public static readonly Error AlreadyExists = new(
                "GroupTaskUpdate.AlreadyExists",
                "Апдейт задачи уже существует");

        }

        public static class GroupTask
        {
            public static readonly Error NotFound = new(
                "GroupTask.NotFound",
                "Задача не найдена");
            public static readonly Error СannotParentForItself = new(
                "GroupTask.СannotParentForItself",
                "Задача не может быть родительской для себя");
            public static readonly Error IdEmpty = new(
                "GroupTask.IdEmpty",
                "Задача не имеет Id");
            public static readonly Error AlreadyExist = new(
                "GroupTask.AlreadyExist",
                "Эта задача уже существует");
        }

        public static class Group
        {
            public static readonly Error UserAlreadyInGroup = new(
                "Group.UserAlreadyInGroup",
                "Пользователь уже в группе");
            public static readonly Error UserNotFound = new(
                "Group.UserNotFound",
                "Пользователь не найден в группе");
            public static readonly Error RoleAlreadyExists = new(
                "Group.RoleAlreadyExists",
                "Роль уже существует в группе");
            public static readonly Error CantDeleteBaseRole = new(
                "Group.CantDeleteBaseRole",
                "Невозможно удалить базовую роль");
            public static readonly Error InviteAlreadySent = new(
                "Group.InviteAlreadySent",
                "Приглашение уже отправлено этому пользователю");
            public static readonly Error InviteNotFound = new(
                "Group.InviteNotFound",
                "Приглашение не найдено");
            public static readonly Error NotFound = new(
                "Group.NotFound",
                "Запись о группе не найдена");
            public static readonly Error IdEmpty = new(
                "Group.IdEmpty",
                "Группа не имеет Id");
            public static readonly Error AlreadyExists = new(
                "Group.AlreadyExists",
                "Группа уже существует");
        }

        public static class PersonalChat
        {
            public static readonly Error SameUser = new(
                "PersonalChat.SameUser",
                "Пользователь1 и Пользователь2 совпадают");
            public static readonly Error AlreadyExists = new(
                "PersonalChat.AlreadyExists",
                "Персональный чат уже существует");
            public static readonly Error NotFound = new(
                "PersonalChat.NotFound",
                "Запись о персональном чате не найдена");
            public static readonly Error IdEmpty = new(
                "PersonalChat.IdEmpty",
                "Персональный чат не имеет Id");
        }

        public static class PersonalMessage
        {
            public static readonly Error AlreadyExists = new(
                "PersonalMessage.AlreadyExists",
                "Персональныое сообщение уже существует");
            public static readonly Error NotFound = new(
                "PersonalMessage.NotFound",
                "Запись о персональном сообщении не найдена");
            public static readonly Error IdEmpty = new(
                "PersonalMessage.IdEmpty",
                "Персональное сообщение не имеет Id");
        }

        public static class SystemRole
        {
            public static readonly Error NotFound = new(
                "SystemRole.NotFound",
                "Запись о роли в системе не найдена");
            public static readonly Error TitleAlreadyInUse = new(
                "SystemRole.AlreadyExist",
                "Запись о роли в системе с таким названием уже существует");
            public static readonly Error NotUniqueName = new(
                "SystemRole.NotUniqueName",
                "Название системной роли не уникально");
            public static readonly Error IdEmpty = new(
                "SystemRole.IdEmpty",
                "Системная роль не имеет Id");
            public static readonly Error AlreadyExists = new(
                "SystemRole.AlreadyExists",
                "Системная роль уже существует");
        }

        public static class UserInGroup
        {
            public static readonly Error NotFound = new(
                "UserInGroup.NotFound",
                "Запись о присутствии пользователя в группе не найдена");
            public static readonly Error AlreadyExists = new(
                "UserInGroup.AlreadyExists",
                "Пользователь уже уастник группы");
        }

        public static class User
        {
            public static readonly Error EmailAlreadyInUse = new(
                "User.EmailAlreadyInUse",
                "Почта уже используется");
            public static readonly Error PhoneNumberAlreadyInUse = new(
                "User.PhoneNumberAlreadyInUse",
                "Телефон уже используется");
            public static readonly Error UsernameAlreadyInUse = new(
                "User.UsernameAlreadyInUse",
                "Имя пользователя уже используется");
            public static readonly Error IncorrectUsernameOrPassword = new(
    "           User.IncorrectUsernameOrPassword",
                "Неправильное имя пользователя или пароль");
            public static readonly Error NotFound = new(
                "User.NotFound",
                "Пользователь не найден");
            public static readonly Error IdEmpty = new(
                "User.IdEmpty",
                "Пользователь не имеет Id");
            public static readonly Error NotUniqueUsername = new(
                "User.NotUniqueUsername",
                "Username пользователя не уникален");
            public static readonly Error NotUniqueEmail = new(
                "User.NotUniqueEmail",
                "Email пользователя не уникален");
            public static readonly Error NotUniquePhoneNumber = new(
                "User.NotUniquePhoneNumber",
                "Телефонный номер пользователя не уникален");
            public static readonly Error AlreadyExists = new(
                "User.AlreadyExists",
                "Пользователь уже существует");
        }
    }
}