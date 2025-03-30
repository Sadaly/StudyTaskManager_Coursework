using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Email
        {
            public static readonly Error Empty = new(
                "Email.Empty",
                "Строка почты пуста");

            public static readonly Error InvalidFormat = new(
                "Email.InvalidFormat",
                "Строка почты имеет неправильный вид");

			public static readonly Error AlreadyVerified = new(
				"Email.AlreadyVerified",
				"Почта уже подтверждена");
		}

        public static class Username
        {
            public static readonly Error Empty = new(
                "Username.Empty",
                "Имя пользователя пусто");

            public static readonly Error TooLong = new(
                "Username.TooLong",
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

			public static readonly Error InvalidFormat = new(
				"PhoneNumber.InvalidFormat",
				"Телефон имеет неверный формат или размер");

			public static readonly Error AlreadyVerified = new(
				"PhoneNumber.AlreadyVerified",
				"Телефон уже подтвержден");
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

			public static readonly Error Match = new(
				"Password.Match",
					"Новый пароль совпадает с текущим");
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
    }
}
