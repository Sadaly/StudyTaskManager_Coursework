using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Log;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Logf
{
    class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(TableNames.Log);

            builder.HasKey(l => l.Id);

            builder
                .HasOne(l => l.LogAction)
                .WithMany()
                .HasForeignKey(l => l.LogActionId);
            builder
                .HasOne(l => l.Initiator)
                .WithMany()
                .HasForeignKey(l => l.InitiatorId);
            builder
                .HasOne(l => l.Subject)
                .WithMany()
                .HasForeignKey(l => l.SubjectId);
            builder
                .HasOne(l => l.Group)
                .WithMany()
                .HasForeignKey(l => l.GroupId);
        }
    }
}
