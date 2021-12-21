using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HospitalExitMap : IEntityTypeConfiguration<HospitalExit>
    {
        public void Configure(EntityTypeBuilder<HospitalExit> builder)
        {
            builder.ToTable("tbl_HospitalExit");
            builder.HasKey(k => k.HospitalExitId);

            #region Properties

            builder.Property(p => p.Reference)
               .HasColumnName("Reference")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.Time)
                .HasColumnName("Time")
                .IsRequired();

            builder.Property(p => p.PurposeofAdmission)
              .HasColumnName("PurposeofAdmission")
              .IsRequired();

            builder.Property(p => p.ConditionOnDischarge)
             .HasColumnName("ConditionOnDischarge")
             .IsRequired();

            builder.Property(p => p.NumberOfStaffRequiredOnDischarge)
             .HasColumnName("NumberOfStaffRequiredOnDischarge")
             .IsRequired();

            builder.Property(p => p.IsGrosSriesAvaible)
             .HasColumnName("IsGrosSriesAvaible")
             .IsRequired();

            builder.Property(p => p.IsHomeCleaned)
             .HasColumnName("IsHomeCleaned")
             .IsRequired();

            builder.Property(p => p.IsMedicationAvaialable)
             .HasColumnName("IsMedicationAvaialable")
             .IsRequired();

            builder.Property(p => p.IsServiceUseronRota)
             .HasColumnName("IsServiceUseronRota")
             .IsRequired();

            builder.Property(p => p.isRotaTeamInformed)
             .HasColumnName("isRotaTeamInformed")
             .IsRequired();

            builder.Property(p => p.isLittleCashAvailableForServiceUser)
             .HasColumnName("isLittleCashAvailableForServiceUser")
             .IsRequired();

            builder.Property(p => p.ModeOfMeansOfTrasportBackHome)
             .HasColumnName("ModeOfMeansOfTrasportBackHome")
             .IsRequired();

            builder.Property(p => p.URLLINK)
             .HasColumnName("URLLINK")
             .IsRequired();

            builder.Property(p => p.AreEqipmentNeededAvailable)
             .HasColumnName("AreEqipmentNeededAvailable")
             .IsRequired();

            builder.Property(p => p.AreStaffTrainnedOnEquipmentNeeded)
             .HasColumnName("AreStaffTrainnedOnEquipmentNeeded")
             .IsRequired();

            builder.Property(p => p.AreContinentProductNeedAndAvailable)
             .HasColumnName("AreContinentProductNeedAndAvailable")
             .IsRequired();

            builder.Property(p => p.AreLocalSupportOrProgramNeeded)
             .HasColumnName("AreLocalSupportOrProgramNeeded")
             .IsRequired();

            builder.Property(p => p.WhichSupportIsNeeded)
             .HasColumnName("WhichSupportIsNeeded")
             .IsRequired();
            builder.Property(p => p.IsCarePlanUpdated)
             .HasColumnName("IsCarePlanUpdated")
             .IsRequired();

            builder.Property(p => p.ReablementRequired)
             .HasColumnName("ReablementRequired")
             .IsRequired();

            builder.Property(p => p.ContactIncaseOfReAdmission)
             .HasColumnName("ContactIncaseOfReAdmission")
             .IsRequired();

            builder.Property(p => p.Remark)
             .HasColumnName("Remark")
             .IsRequired();

            builder.Property(p => p.Status)
             .HasColumnName("Status")
             .IsRequired();


            #endregion

            #region Relationship
            builder.HasMany<HospitalExitOfficerToTakeAction>(p => p.OfficerToTakeAction)
                .WithOne(p => p.HospitalExit)
                .HasForeignKey(p => p.HospitalExitId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
