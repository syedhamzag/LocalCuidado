using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AwesomeCare.Model.Models.Map
{
    public class RotaDayofWeekMap : IEntityTypeConfiguration<RotaDayofWeek>
    {
        public void Configure(EntityTypeBuilder<RotaDayofWeek> builder)
        {
            builder.ToTable("tbl_RotaDayofWeek");
            builder.HasKey(p => p.RotaDayofWeekId);

            #region Properties
            builder.Property(p => p.RotaDayofWeekId)
                .HasColumnName("RotaDayofWeekId")
                .IsRequired();

            builder.Property(p => p.DayofWeek)
               .HasColumnName("DayofWeek")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(p => p.Deleted)
                .HasColumnName("Deleted")
                .IsRequired();
            #endregion

            #region Seeding
            builder.HasData(new RotaDayofWeek
            {
                Deleted = false,
                DayofWeek = "Monday",
                RotaDayofWeekId = 1
            },
            new RotaDayofWeek
            {
                Deleted = false,
                DayofWeek = "Tuesday",
                RotaDayofWeekId = 2
            },
            new RotaDayofWeek
            {
                Deleted = false,
                DayofWeek = "Wednesday",
                RotaDayofWeekId = 3
            },
            new RotaDayofWeek
            {
                Deleted = false,
                DayofWeek = "Thursday",
                RotaDayofWeekId = 4
            },
            new RotaDayofWeek
            {
                Deleted = false,
                DayofWeek = "Friday",
                RotaDayofWeekId = 5
            },
            new RotaDayofWeek
            {
                Deleted = false,
                DayofWeek = "Saturday",
                RotaDayofWeekId = 6
            },
            new RotaDayofWeek
            {
                Deleted = false,
                DayofWeek = "Sunday",
                RotaDayofWeekId = 7
            });
            #endregion
        }
    }
}
