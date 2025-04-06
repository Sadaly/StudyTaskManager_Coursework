using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Errors
{
    public static class PersistenceErrors
    {
        public static readonly Error UnknowError = new(
            "UnknowError",
            "Неизвестная ошибка");

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
        }

        public static class GroupInvite
        {
            public static readonly Error Accepted = new(
                "GroupInvite.Accepted",
                "Приглашение уже было принято. И не может быть принято или отклонено");

            public static readonly Error Declined = new(
                "GroupInvite.Declined",
                "Приглашение уже было отклонено. И не может быть принято или отклонено");
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

            public static readonly Error RoleNotFound = new(
                "Group.RoleNotFound",
                "Роль не найдена");

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
        }

        public static class UserInGroup
        {
            public static readonly Error NotFound = new(
                "UserInGroup.NotFound",
                "Запись о присутствии пользователя в группе не найдена");

        }
    }
}
