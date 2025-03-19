using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        public const int MAX_LENGTH = 50;
        public const int MIN_LENGTH = 8;
        private Password(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        /// <summary>
        /// Создание экземпляра <see cref="Password"/> с проверкой входящих значений
        /// </summary>
        /// <param name="password">Строка с паролем</param>
        /// <returns>Новый экземпляр <see cref="Password"/></returns>
        public static Result<Password> Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<Password>(DomainErrors.Password.Empty);
            }

            if (password.Length > MAX_LENGTH)
            {
                return Result.Failure<Password>(DomainErrors.Password.TooLong);
            }

            if (password.Length < MIN_LENGTH)
            {
                return Result.Failure<Password>(DomainErrors.Password.TooShort);
            }

            return new Password(password);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
