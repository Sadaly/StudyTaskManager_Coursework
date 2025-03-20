using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
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
        private GroupInvite(User.User sender, User.User receiver, Group group) : base()
        {
            Sender = sender;
            SenderId = sender.Id;

            Receiver = receiver;
            ReceiverId = receiver.Id;

            Group = group;
            GroupId = group.Id;

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
        /// Дата отправки приглашения.
        /// </summary>
        public DateTime DateInvitation { get; }

        /// <summary>
        /// ID группы, в которую приглашают.
        /// </summary>
        public Guid GroupId { get; }

        /// <summary>
        /// Флаг принятия приглашения (false — не принято, true — принято).
        /// </summary>
        public bool? InvitationAccepted { get; private set; }

        /// <summary>
        /// Отправитель приглашения.
        /// </summary>
        public User.User Sender { get; }

        /// <summary>
        /// Получатель приглашения.
        /// </summary>
        public User.User Receiver { get; }

        /// <summary>
        /// Группа, в которую приглашают.
        /// </summary>
        public Group Group { get; }

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


            this.RaiseDomainEvent(new GroupInviteAcceptedDomainEvent(this.GroupId, this.ReceiverId));

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

			this.RaiseDomainEvent(new GroupInviteDeclinedDomainEvent(this.GroupId, this.ReceiverId));

			return Result.Success();
        }

        /// <summary>
        /// Создает новое приглашение.
        /// </summary>
        public static GroupInvite Create(User.User sender, User.User receiver, Group group)
        {
			var invite = new GroupInvite(sender, receiver, group);

			invite.RaiseDomainEvent(new GroupInviteCreatedDomainEvent(invite.GroupId, invite.ReceiverId));

			return invite;
		}
    }
}
