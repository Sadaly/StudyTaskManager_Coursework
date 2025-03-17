using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.ValueObjects
{
    public class UserName : ValueObject
    {
        public const int MAX_LENGTH = 50;

        private UserName(string value)
        {
            Value = value;
        }

        public string Value { get; set; }


        /// <summary>
        /// Создание экземпляра класса с проверкой входящих значений
        /// </summary>
        /// <param name="username">Строка с именем</param>
        /// <returns></returns>
        public static Result<UserName> Create(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Result.Failure<UserName>(DomainErrors.UserName.Empty);
            }

            if (username.Length > MAX_LENGTH)
            {
                return Result.Failure<UserName>(DomainErrors.UserName.TooLong);
            }

            return new UserName(username);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
