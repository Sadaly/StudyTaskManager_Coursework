using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Log;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Logf
{
    class LogActionConfiguration : IEntityTypeConfiguration<LogAction>
    {
        public void Configure(EntityTypeBuilder<LogAction> builder)
        {
            builder.ToTable(TableNames.LogAction);

            builder.HasKey(l => l.Id);
        }
    }
}
