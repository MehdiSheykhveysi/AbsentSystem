using AbsentSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbsentSystem.Data.Configurations
{
    public class VacationsConfig : IEntityTypeConfiguration<TypeVacation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TypeVacation> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();
            builder.Property(v => v.Title).HasMaxLength(100);
            builder.Property(v => v.DepartureTime).HasMaxLength(8);
            builder.Property(v => v.EntranceTime).HasMaxLength(8);
        }
    }
}
