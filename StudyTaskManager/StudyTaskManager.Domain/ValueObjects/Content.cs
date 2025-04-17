using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.ValueObjects
{
    public class Content : ValueObject
    {
        public const int MAX_LENGTH = 5000;
        private Content(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        /// <summary>
        /// Создание экземпляра <see cref="Content"/> с проверкой входящих значений
        /// </summary>
        /// <param name="stringText">Строка с текстом</param>
        /// <returns>Новый экземпляр <see cref="Content"/></returns>
        public static Result<Content> Create(string stringText)
        {
            if (string.IsNullOrWhiteSpace(stringText))
            {
                return Result.Failure<Content>(DomainErrors.Content.Empty);
            }

            if (stringText.Length > MAX_LENGTH)
            {
                return Result.Failure<Content>(DomainErrors.Content.TooLong);
            }

            return new Content(stringText);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
