using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Log
{
    /// <summary>
    /// Действия для логов
    /// </summary>
    public class LogAction : BaseEntityWithID
    {
        private LogAction(Guid id, string Name, string Description) : base(id)
        {
            this.Name = Name;
            this.Description = Description;
        }

        /// <summary>
        /// Название действия
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Описание действия
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <returns>Новый экземпляр класс <see cref="LogAction"/></returns>
        public LogAction Create(Guid id, string Name, string Description)
        {
            var LogAction = new LogAction(id, Name, Description);

            //Todo создание события

            return LogAction;
        }
    }
}
