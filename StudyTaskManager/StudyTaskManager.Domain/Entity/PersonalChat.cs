namespace StudyTaskManager.Domain.Entity
{
    class PersonalChat
    {
        public User User1 { get; }
        public User User2 { get; }
        public IReadOnlyCollection<Message> Messages => _messages;
        private List<Message> _messages;
    }
}
