using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class InvestigationAttachmentMap : IEntityTypeConfiguration<InvestigationAttachment>
    {
        public void Configure(EntityTypeBuilder<InvestigationAttachment> builder)
        {
            builder.ToTable("tbl_InvestigationAttachment");
            builder.HasKey(k => k.InvestigationAttachmentId);

            #region Properties

            builder.Property(p => p.InvestigationAttachmentId)
                .HasColumnName("InvestigationAttachmentId")
                .IsRequired();

            builder.Property(p => p.InvestigationId)
               .HasColumnName("InvestigationId")
               .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(o => o.Investigation)
                .WithMany(m => m.InvestigationAttachments)
                .HasForeignKey(f => f.InvestigationId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
