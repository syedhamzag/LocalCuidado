using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffCommunicationMap : IEntityTypeConfiguration<StaffCommunication>
    {
        public void Configure(EntityTypeBuilder<StaffCommunication> builder)
        {
            builder.ToTable("tbl_StaffCommunication");
            builder.HasKey(p => p.StaffCommunicationId);

            #region Properties
            builder.Property(p => p.StaffCommunicationId)
                .HasColumnName("StaffCommunicationId")
                .IsRequired();

            builder.Property(p => p.ValueDate)
              .HasColumnName("ValueDate")
               .HasColumnType("datetime2")
              .IsRequired();

            builder.Property(p => p.Concern)
              .HasColumnName("Concern")
              .HasMaxLength(500)
              .IsRequired();

            builder.Property(p => p.CommunicationClassId)
              .HasColumnName("CommunicationClass")
              .IsRequired();

            builder.Property(p => p.PersonInvolved)
              .HasColumnName("PersonInvolved")
              .IsRequired();

            builder.Property(p => p.ExpectedAction)
              .HasColumnName("ExpectedAction")
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(p => p.ActionTaken)
              .HasColumnName("ActionTaken")
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(p => p.Status)
             .HasColumnName("Status")
             .IsRequired();

            builder.Property(p => p.Telephone)
             .HasColumnName("Telephone")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.PersonResponsibleForAction)
             .HasColumnName("PersonResponsibleForAction")
             .IsRequired();

            builder.Property(p => p.Attachment)
           .HasColumnName("Attachment")
           .IsRequired(false);
            #endregion
        }
    }
}
