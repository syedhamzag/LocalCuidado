using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffPersonalInfoCommentMap : IEntityTypeConfiguration<StaffPersonalInfoComment>
    {
        public void Configure(EntityTypeBuilder<StaffPersonalInfoComment> builder)
        {
            builder.ToTable("tbl_StaffPersonalInfoComment");
            builder.HasKey(k => k.StaffPersonalInfoCommentId);

            #region Properties
            builder.Property(p => p.StaffPersonalInfoCommentId)
                .HasColumnName("StaffPersonalInfoCommentId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.CommentBy_UserId)
               .HasColumnName("CommentBy_UserId")
               .IsRequired(false);

            builder.Property(p => p.Comment)
               .HasColumnName("Comment")
               .HasMaxLength(250)
               .IsRequired();

            builder.Property(p => p.CommentOn)
               .HasColumnName("CommentOn")
               .HasColumnType("datetime2")
               .IsRequired();


            #endregion

            #region Relationship
            builder.HasOne<StaffPersonalInfo>(p => p.StaffPersonalInfo)
                .WithMany(p => p.StaffPersonalInfoComments)
                .HasForeignKey(f => f.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
