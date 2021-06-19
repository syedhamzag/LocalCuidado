using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeCare.Model.Models.Map
{
    public class EnoticeMap : IEntityTypeConfiguration<Enotice>
    {
        public void Configure(EntityTypeBuilder<Enotice> builder)
        {
            builder.ToTable("tbl_Enotice");
            builder.HasKey(k => k.EnoticeId);

            #region Properties
            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.PublishTo)
               .HasColumnName("PublishTo")
               .IsRequired();

            builder.Property(p => p.Heading)
               .HasColumnName("Heading")
               .IsRequired();

            builder.Property(p => p.Note)
               .HasColumnName("Note")
               .IsRequired();

            builder.Property(p => p.PublishBy)
               .HasColumnName("PublishBy")
               .IsRequired();

            builder.Property(p => p.Image)
               .HasColumnName("Image")
               .IsRequired();

            builder.Property(p => p.Video)
               .HasColumnName("Video")
               .IsRequired();
            #endregion

            #region Relationship

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.Enotice)
                 .HasForeignKey(p => p.PublishTo)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}