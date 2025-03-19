using System.Security.Cryptography;
using System.Text;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        private PasswordHash(string hashedPassword)
        {
            Value = hashedPassword;
        }

        public string Value { get; set; }

        /// <summary>
        /// Создание экземпляра <see cref="PasswordHash"/>  с проверкой входящих значений
        /// </summary>
        /// <param name="password"><see cref="Password"/> со строкой пароля</param>
        /// <returns>Новый экземпляр <see cref="PasswordHash"/></returns>
        public static Result<PasswordHash> Create(Password password)
        {
            string hashedPassword = HashPassword(password.Value);
            return new PasswordHash(hashedPassword);
        }

        /// <summary>
        /// Hash-функция
        /// </summary>
        /// <param name="password">Пароль, который будет захэширован</param>
        /// <returns>Hash пароля</returns>
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Преобразуем байты в hex-строку
                }
                return builder.ToString();
            }
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
