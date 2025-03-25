using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using System;

namespace StudyTaskManager.Domain.Entity.Group
{
    /// <summary>
    /// Приглашение пользователя в группу.
    /// </summary>
    public class GroupInvite : BaseEntity
    {
        /// <summary>
        /// Приватный конструктор для создания объекта <see cref="GroupInvite"/>.
        /// </summary>
        private GroupInvite(Guid senderId, Guid receiverId, Guid groupId) : base()
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            GroupId = groupId;

            DateInvitation = DateTime.UtcNow;
        }

        /// <summary>
        /// ID отправителя приглашения.
        /// </summary>
        public Guid SenderId { get; }

        /// <summary>
        /// ID получателя приглашения.
        /// </summary>
        public Guid ReceiverId { get; }

        /// <summary>
        /// ID группы, в которую приглашают.
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Дата отправки приглашения.
        /// </summary>
        public DateTime DateInvitation { get; }

        /// <summary>
        /// Флаг принятия приглашения (false — не принято, true — принято).
        /// </summary>
        public bool? InvitationAccepted { get; private set; }

        /// <summary>
        /// Отправитель приглашения.
        /// </summary>
        public User.User? Sender { get; private set; }

        /// <summary>
        /// Получатель приглашения.
        /// </summary>
        public User.User? Receiver { get; private set; }

        /// <summary>
        /// Группа, в которую приглашают.
        /// </summary>
        public Group? Group { get; private set; }

        /// <summary>
        /// Метод для принятия приглашения.
        /// </summary>
        public Result AcceptInvite()
        {
            if (InvitationAccepted != null)
                if (InvitationAccepted == true)
                    return Result.Failure(DomainErrors.GroupInvite.Accepted);
                else
                    return Result.Failure(DomainErrors.GroupInvite.Declined);

            InvitationAccepted = true;

            return Result.Success();
        }

        /// <summary>
        /// Метод для отклонения приглашения.
        /// </summary>
        public Result DeclineInvite()
        {
            if (InvitationAccepted != null)
                if (InvitationAccepted == true)
                    return Result.Failure(DomainErrors.GroupInvite.Accepted);
                else
                    return Result.Failure(DomainErrors.GroupInvite.Declined);

            InvitationAccepted = false;

            return Result.Success();
        }

        /// <summary>
        /// Создает новое приглашение.
        /// </summary>
        public static GroupInvite Create(User.User sender, User.User receiver, Group group)
        {
            return new GroupInvite(sender.Id, receiver.Id, group.Id)
            {
                Sender = sender,
                Receiver = receiver,
                Group = group
            };
        }
    }
}
