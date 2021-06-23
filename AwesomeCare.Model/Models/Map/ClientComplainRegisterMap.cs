using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientComplainRegisterMap : IEntityTypeConfiguration<ClientComplainRegister>
    {
        public void Configure(EntityTypeBuilder<ClientComplainRegister> builder)
        {
            builder.ToTable("tbl_Client_ComplainRegister");
            builder.HasKey(k => k.ComplainId);

            #region Properties
            builder.Property(p => p.Reference)
               .HasColumnName("Reference")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.LINK)
                .HasColumnName("LINK")
                .HasMaxLength(255)
                .IsRequired();

        builder.Property(p => p.IRFNUMBER)
               .HasColumnName("IRFNUMBER ")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.INCIDENTDATE)
               .HasColumnName("INCIDENTDATE")
               .IsRequired();

            builder.Property(p => p.DATERECIEVED)
               .HasColumnName("DATERECIEVED")
               .IsRequired();

            builder.Property(p => p.DATEOFACKNOWLEDGEMENT)
               .HasColumnName("DATEOFACKNOWLEDGEMENT");

            builder.Property(p => p.OFFICERTOACTId)
               .HasColumnName("OFFICERTOACTId")
               .IsRequired();

            builder.Property(p => p.SOURCEOFCOMPLAINTS)
               .HasColumnName("SOURCEOFCOMPLAINTS")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.COMPLAINANTCONTACT)
               .HasColumnName("COMPLAINANTCONTACT")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.STAFFId)
               .HasColumnName("STAFFId")
               .IsRequired();

            builder.Property(p => p.CONCERNSRAISED)
               .HasColumnName("CONCERNSRAISED")
               .HasMaxLength(255)
               .IsRequired();
        
            builder.Property(p => p.DUEDATE)
               .HasColumnName("DUEDATE")
               .IsRequired();

            builder.Property(p => p.LETTERTOSTAFF)
               .HasColumnName("LETTERTOSTAFF")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.INVESTIGATIONOUTCOME)
               .HasColumnName("INVESTIGATIONOUTCOME")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ACTIONTAKEN)
               .HasColumnName("ACTIONTAKEN")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.FINALRESPONSETOFAMILY)
               .HasColumnName("FINALRESPONSETOFAMILY")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ROOTCAUSE)
             .HasColumnName("ROOTCAUSE")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.REMARK)
               .HasColumnName("REMARK")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.StatusId)
               .HasColumnName("StatusId")
               .IsRequired();

            builder.Property(p => p.EvidenceFilePath)
             .HasColumnName("EvidenceFilePath")
             .IsRequired();

            #endregion
        }
    }
}
