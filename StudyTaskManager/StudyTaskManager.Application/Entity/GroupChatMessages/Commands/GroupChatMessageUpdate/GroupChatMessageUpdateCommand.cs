using StudyTaskManager.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageUpdate;
public sealed record GroupChatMessageUpdateCommand(Guid GroupChatId, ulong Ordinal, string Content) : ICommand<(Guid, ulong)>;
