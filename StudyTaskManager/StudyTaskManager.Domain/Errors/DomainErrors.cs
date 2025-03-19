using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly Error EmailAlreadyInUse = new(
                "User.EmailAlreadyInUse",
                "Почта уже используется");

            public static readonly Error PhoneNumberAlreadyInUse = new(
                "User.PhoneNumberAlreadyInUse",
                "Телефон уже используется");

            public static readonly Error UserNameAlreadyInUse = new(
                "User.UserNameAlreadyInUse",
                "Имя пользовтеля уже используется");
        }

        public static class Email
        {
            public static readonly Error Empty = new(
                "Email.Empty",
                "Строка почты пуста");

            public static readonly Error InvalidFormat = new(
                "Email.InvalidFormat",
                "Строка почты имеет неправильный вид");
        }

        public static class UserName
        {
            public static readonly Error Empty = new(
                "UserName.Empty",
                "Имя пользователя пусто");

            public static readonly Error TooLong = new(
                "UserName.TooLong",
                "Имя пользователя слишком длинное");
        }

        public static class PhoneNumber
        {
            public static readonly Error Empty = new(
                "PhoneNumber.Empty",
                "Телефон пуст");

            public static readonly Error TooLong = new(
                "PhoneNumber.TooLong",
                "Телефон слишком длинный");

            public static readonly Error TooShort = new(
                "PhoneNumber.TooLong",
                "Телефон слишком короткий");
        }

        public static class Password
        {
            public static readonly Error Empty = new(
                "Password.Empty",
                "Пароль пуст");

            public static readonly Error TooLong = new(
                "Password.TooLong",
                "Пароль слишком длинный");

            public static readonly Error TooShort = new(
                "Password.TooLong",
                "Пароль слишком короткий");
        }

        public static class Content
        {
            public static readonly Error Empty = new(
                "Content.Empty",
                "Содержание пусто");

            public static readonly Error TooLong = new(
                "Content.TooLong",
                "Содержание слишком длинное");
        }

        public static class Title
        {
            public static readonly Error Empty = new(
                "Title.Empty",
                "Название пусто");

            public static readonly Error TooLong = new(
                "Title.TooLong",
                "Название слишком длинное");
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
        }

        public static class PersonalChat
        {
            public static readonly Error SameUser = new(
                "PersonalChat.SameUser",
                "Пользователь1 и Пользователь2 совпадают");

        }
    }
}
