using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRatingMap : IEntityTypeConfiguration<StaffRating>
    {
        public void Configure(EntityTypeBuilder<StaffRating> builder)
        {
            builder.ToTable("tbl_StaffRating");
            builder.HasKey(k => k.StaffRatingId);

            #region Properties

            builder.Property(p => p.StaffRatingId)
               .HasColumnName("StaffRatingId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.ClientId)
             .HasColumnName("ClientId")
             .IsRequired();

            builder.Property(p => p.Comment)
            .HasColumnName("Comment")
            .IsRequired();

            builder.Property(p => p.CommentDate)
            .HasColumnName("CommentDate")
            .IsRequired();

            builder.Property(p => p.Rating)
            .HasColumnName("Rating")
            .IsRequired();

            builder.Property(p => p.SubmittedBy)
              .HasColumnName("SubmittedBy")
              .IsRequired();
            
            #endregion

            #region Relationship
            builder.HasOne(b => b.StaffPersonalInfo)
                .WithMany(b => b.StaffRating)
                .HasForeignKey(b => b.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
