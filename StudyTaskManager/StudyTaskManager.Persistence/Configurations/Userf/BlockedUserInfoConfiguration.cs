using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Userf
{
    class BlockedUserInfoConfiguration : IEntityTypeConfiguration<BlockedUserInfo>
    {
        public void Configure(EntityTypeBuilder<BlockedUserInfo> builder)
        {
            builder.ToTable(TableNames.BlockedUserInfo);

            // Внешний ключ для пользователя
            builder
                .HasOne(bui => bui.User)
                .WithOne();
            //.HasForeignKey(bui => bui.UserId);

            // Внешний ключ для предыдущей роли
            builder
                .HasOne(bui => bui.PrevRole)
                .WithMany()
                .HasForeignKey(bui => bui.PrevRoleId);

            //builder
            //    .Property(bui => bui.UserId)
            //    .IsRequired()
            //    .HasColumnName(TableNames.BlockedUserInfoTable.UserId);
            //builder
            //    .Property(bui => bui.PrevRoleId)
            //    .HasColumnName(TableNames.BlockedUserInfoTable.PrevRoleId);
        }
    }
}
