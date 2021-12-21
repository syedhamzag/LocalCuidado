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
                .IsRequired();

        builder.Property(p => p.IRFNUMBER)
               .HasColumnName("IRFNUMBER ")
               .IsRequired();

            builder.Property(p => p.INCIDENTDATE)
               .HasColumnName("INCIDENTDATE")
               .IsRequired();

            builder.Property(p => p.DATERECIEVED)
               .HasColumnName("DATERECIEVED")
               .IsRequired();

            builder.Property(p => p.DATEOFACKNOWLEDGEMENT)
               .HasColumnName("DATEOFACKNOWLEDGEMENT");

            builder.Property(p => p.SOURCEOFCOMPLAINTS)
               .HasColumnName("SOURCEOFCOMPLAINTS")
               .IsRequired();

            builder.Property(p => p.COMPLAINANTCONTACT)
               .HasColumnName("COMPLAINANTCONTACT")
               .IsRequired();

            builder.Property(p => p.CONCERNSRAISED)
               .HasColumnName("CONCERNSRAISED")
               .IsRequired();
        
            builder.Property(p => p.DUEDATE)
               .HasColumnName("DUEDATE")
               .IsRequired();

            builder.Property(p => p.LETTERTOSTAFF)
               .HasColumnName("LETTERTOSTAFF")
               .IsRequired();

            builder.Property(p => p.INVESTIGATIONOUTCOME)
               .HasColumnName("INVESTIGATIONOUTCOME")
               .IsRequired();

            builder.Property(p => p.ACTIONTAKEN)
               .HasColumnName("ACTIONTAKEN")
               .IsRequired();

            builder.Property(p => p.FINALRESPONSETOFAMILY)
               .HasColumnName("FINALRESPONSETOFAMILY")
               .IsRequired();

            builder.Property(p => p.ROOTCAUSE)
             .HasColumnName("ROOTCAUSE")
             .IsRequired();

            builder.Property(p => p.REMARK)
               .HasColumnName("REMARK")
               .IsRequired();

            builder.Property(p => p.StatusId)
               .HasColumnName("StatusId")
               .IsRequired();

            builder.Property(p => p.EvidenceFilePath)
             .HasColumnName("EvidenceFilePath");
            #endregion

            #region RelationShip

            builder.HasMany<ComplainStaffName>(p => p.StaffName)
                .WithOne(p => p.ComplainRegister)
                .HasForeignKey(p => p.ComplainId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ComplainOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.ComplainRegister)
                .HasForeignKey(p => p.ComplainId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
