using AbsentSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbsentSystem.Data.Configurations
{
    public class AttendanceConfig : IEntityTypeConfiguration<AttendanceList>
    {
        public void Configure(EntityTypeBuilder<AttendanceList> builder)
        {
            builder.HasKey(a => new { a.Id });
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.HasMany(a => a.Vacations).WithOne(a => a.AttendanceList).HasForeignKey(a => a.AttendanceListId);
        }
    }
}
