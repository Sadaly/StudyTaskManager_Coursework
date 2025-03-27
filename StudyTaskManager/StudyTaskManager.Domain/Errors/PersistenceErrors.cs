using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Errors
{
    public static class PersistenceErrors
	{
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
        }
    }
}
