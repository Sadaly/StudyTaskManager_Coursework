namespace StudyTaskManager.Domain.Entity
{
    public class PersonalChat
    {
        public User User1 { get; } = null!;
        public User User2 { get; } = null!;
        public IReadOnlyCollection<PersonalMessage>? Messages => _messages;
        private List<PersonalMessage>? _messages;
    }
}
