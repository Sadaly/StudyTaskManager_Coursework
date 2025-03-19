using System.Text.RegularExpressions;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        //Международный стандарт
        public const int MAX_LENGTH = 15;
        //Международный стандарт
        public const int MIN_LENGTH = 8;
        private PhoneNumber(string phoneNumber)
        {
            Value = phoneNumber;
        }

        public string Value { get; set; }

        /// <summary>
        /// Создание экземпляра <see cref="PhoneNumber"/> с проверкой входящих значений
        /// </summary>
        /// <param name="phoneNumber">Строка с номером телефона</param>
        /// <returns>Новый экземпляр <see cref="PhoneNumber"/></returns>
        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            var cleanedNumber = Regex.Replace(phoneNumber, @"[^0-9+]", "");

            if (string.IsNullOrWhiteSpace(cleanedNumber))
            {
                return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.Empty);
            }

            if (cleanedNumber.Length > MAX_LENGTH)
            {
                return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.TooLong);
            }

            if (cleanedNumber.Length < MIN_LENGTH)
            {
                return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.TooShort);
            }

            return new PhoneNumber(cleanedNumber);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
