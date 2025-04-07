using StudyTaskManager.Domain.Shared;
using System.Threading.Tasks;

namespace StudyTaskManager.Domain.Errors
{
    public static class PersistenceErrors
    {
        public static readonly Error UnknowError = new(
            "UnknowError",
            "Неизвестная ошибка");

        public static class GroupChatMessage
        {
            public static readonly Error NotFound = new(
                "GroupChatMessage.NotFound",
                "Сообщение в групповом чате не найдено");
        }

        public static class GroupChatParticipant
        {
            public static readonly Error NotFound = new(
                "GroupChatParticipant.NotFound",
                "Пользователь не найден");
            public static readonly Error UserDoesNotBelongToTheGroupChat = new(
                "GroupChatParticipant.UserDoesNotBelongToTheGroupChat",
                "Пользователь не принадлежит групповому чату");
            public static readonly Error AddingToAPublicChat = new(
                "GroupChatParticipant.AddingToAPublicChat",
                "Попытка добавления к публичному чату");
        }

        public static class GroupChat
        {
            public static readonly Error NotFound = new(
                "GroupChat.NotFound",
                "Пользователь не найден");
            public static readonly Error NotUniqueName = new(
                "GroupChat.NotUniqueName",
                "Название группового чата не уникально");
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
        }

        public static class GroupRole
        {
            public static readonly Error NotFound = new(
                "Group.NotFound",
                "Роль не найдена");
            public static readonly Error NotUniqueName = new(
                "GroupRole.NotUniqueName",
                "Название групповой роли не уникально");
        }

        public static class GroupTaskStatus
        {
            public static readonly Error NotFound = new(
                "GroupTaskStatus.NotFound",
                "Статус задачи не найден");
            public static readonly Error CantBeUpdated = new(
                "GroupTaskStatus.CantBeUpdated",
                "Статус задачи не позволяет создавать новые апдейты");
        }
        
        public static class GroupTask
        {
            public static readonly Error NotFound = new(
                "GroupTask.NotFound",
                "Задача не найдена");
            public static readonly Error СannotParentForItself = new (
                "GroupTask.СannotParentForItself",
                "Задача не может быть родительской для себя");
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
        }

        public static class PersonalChat
        {
            public static readonly Error SameUser = new(
                "PersonalChat.SameUser",
                "Пользователь1 и Пользователь2 совпадают");
            public static readonly Error AlreadyExists= new (
                "PersonalChat.AlreadyExists",
                "Персональный чат уже существует");
            public static readonly Error NotFound = new(
                "PersonalChat.NotFound",
                "Запись о персональном чате не найдена");
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
        }

        public static class UserInGroup
        {
            public static readonly Error NotFound = new(
                "UserInGroup.NotFound",
                "Запись о присутствии пользователя в группе не найдена");
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
    "User.IncorrectUsernameOrPassword",
    "Неправильное имя пользователя или пароль");
            public static readonly Error NotFound = new(
    "User.NotFound",
    "Пользователь не найден");
            public static readonly Error IdEmpty = new(
    "User.IdEmpty",
    "Пользователь не имеет Id");
            public static readonly Error NotUniqueUsername = new(
                "User.NotUniqueUsername",
                "Username польозвателя не уникален");
            public static readonly Error NotUniqueEmail = new(
                "User.NotUniqueEmail",
                "Email польозвателя не уникален");
        }
    }
}
