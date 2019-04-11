using AbsentSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AbsentSystem.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.PersonelId).HasMaxLength(20);
            builder.Property(u => u.ShowPass).HasMaxLength(50);
            builder.Property(u => u.NationalCode).HasMaxLength(12);
            builder.Property(u => u.Address).HasMaxLength(150);
            builder.Property(u => u.DisplayName).HasMaxLength(50);

            //Relations

            builder.HasMany(u => u.AttendanceLists).WithOne(c => c.User).HasForeignKey(u => u.UserId);
        }
    }
}