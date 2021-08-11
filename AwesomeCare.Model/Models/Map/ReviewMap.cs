using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ReviewMap : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("tbl_Review");
            builder.HasKey(k => k.ReviewId);

            #region Properties

            builder.Property(p => p.ReviewId)
               .HasColumnName("ReviewId")
               .IsRequired();

            builder.Property(p => p.PersonalDetailId)
               .HasColumnName("PersonalDetailId")
               .IsRequired();

            builder.Property(p => p.CP_PreDate)
              .HasColumnName("CP_PreDate")
              .IsRequired();

            builder.Property(p => p.CP_ReviewDate)
             .HasColumnName("CP_ReviewDate")
             .IsRequired();

            builder.Property(p => p.RA_PreDate)
              .HasColumnName("RA_PreDate")
              .IsRequired();

            builder.Property(p => p.RA_ReviewDate)
             .HasColumnName("RA_ReviewDate")
             .IsRequired();

            #endregion
        }
    }
}
