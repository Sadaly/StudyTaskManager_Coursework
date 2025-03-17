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
    }
}
