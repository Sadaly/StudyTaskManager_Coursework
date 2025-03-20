using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Log;

namespace StudyTaskManager.Persistence.DB.Configurations
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
