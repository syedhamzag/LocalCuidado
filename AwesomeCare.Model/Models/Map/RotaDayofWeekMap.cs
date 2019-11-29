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
            #endregion
        }
    }
}
