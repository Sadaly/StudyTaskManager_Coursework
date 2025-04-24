using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyTaskManager.Domain.ValueObjects
{
    [ComplexType]
    public class Username : ValueObject
    {
        public const int MAX_LENGTH = 50;

        //private Username() { Value = ""; }
        private Username(string value)
        {
            Value = value;
        }

        public string Value { get; set; }


        /// <summary>
        /// Создание экземпляра <see cref="Username"/> с проверкой входящих значений
        /// </summary>
        /// <param name="username">Строка с именем</param>
        /// <returns>Новый экземпляр <see cref="Username"/></returns>
        public static Result<Username> Create(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return Result.Failure<Username>(DomainErrors.Username.Empty);
            }

            if (username.Length > MAX_LENGTH)
            {
                return Result.Failure<Username>(DomainErrors.Username.TooLong);
            }

            return new Username(username);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
