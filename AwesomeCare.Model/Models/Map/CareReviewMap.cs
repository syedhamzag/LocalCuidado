using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CareReviewMap : IEntityTypeConfiguration<CareReview>
    {
        public void Configure(EntityTypeBuilder<CareReview> builder)
        {
            builder.ToTable("tbl_CareReview");
            builder.HasKey(k => k.CareReviewId);

            #region Properties
            builder.Property(p => p.Name)
               .HasColumnName("Name")
               .IsRequired();

            builder.Property(p => p.Action)
               .HasColumnName("Action")
               .IsRequired();

            builder.Property(p => p.Note)
               .HasColumnName("Note")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();
            #endregion

            #region Relation
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.CareReview)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}

