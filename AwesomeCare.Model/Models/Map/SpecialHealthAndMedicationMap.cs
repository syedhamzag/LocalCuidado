using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SpecialHealthAndMedicationMap : IEntityTypeConfiguration<SpecialHealthAndMedication>
    {
        public void Configure(EntityTypeBuilder<SpecialHealthAndMedication> builder)
        {
            builder.ToTable("tbl_SpecialHealthAndMedication");
            builder.HasKey(k => k.SHMId);

            #region Properties
            builder.Property(p => p.SHMId)
               .HasColumnName("SHMId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.AccessMedication)
               .HasColumnName("AccessMedication")
               .IsRequired();

            builder.Property(p => p.AdminLvl)
               .HasColumnName("AdminLvl")
               .IsRequired();

            builder.Property(p => p.FamilyMeds)
               .HasColumnName("FamilyMeds")
               .IsRequired();

            builder.Property(p => p.FamilyReturnMed)
               .HasColumnName("FamilyReturnMed")
               .IsRequired();

            builder.Property(p => p.LeftoutMedicine)
               .HasColumnName("LeftoutMedicine")
               .IsRequired();

            builder.Property(p => p.MedAccessDenial)
               .HasColumnName("MedAccessDenial")
               .IsRequired();

            builder.Property(p => p.MedicationAllergy)
               .HasColumnName("MedicationAllergy")
               .IsRequired();

            builder.Property(p => p.MedicationStorage)
               .HasColumnName("MedicationStorage")
               .IsRequired();

            builder.Property(p => p.MedKeyCode)
               .HasColumnName("MedKeyCode")
               .IsRequired();

            builder.Property(p => p.MedsGPOrder)
               .HasColumnName("MedsGPOrder")
               .IsRequired();

            builder.Property(p => p.NameFormMedicaiton)
               .HasColumnName("NameFormMedicaiton")
               .IsRequired();

            builder.Property(p => p.NoMedAccess)
               .HasColumnName("NoMedAccess")
               .IsRequired();

            builder.Property(p => p.OverdoseContact)
               .HasColumnName("OverdoseContact")
               .IsRequired();

            builder.Property(p => p.PharmaMARChart)
               .HasColumnName("PharmaMARChart")
               .IsRequired();

            builder.Property(p => p.PNRDoses)
               .HasColumnName("PNRDoses")
               .IsRequired();

            builder.Property(p => p.PNRMedList)
               .HasColumnName("PNRMedList")
               .IsRequired();

            builder.Property(p => p.PNRMedReq)
               .HasColumnName("PNRMedReq")
               .IsRequired();

            builder.Property(p => p.PNRMedsAdmin)
               .HasColumnName("PNRMedsAdmin")
               .IsRequired();

            builder.Property(p => p.PNRMedsMissing)
               .HasColumnName("PNRMedsMissing")
               .IsRequired();

            builder.Property(p => p.SpecialStorage)
               .HasColumnName("SpecialStorage")
               .IsRequired();

            builder.Property(p => p.TempMARChart)
               .HasColumnName("TempMARChart")
               .IsRequired();

            builder.Property(p => p.WhoAdminister)
               .HasColumnName("WhoAdminister")
               .IsRequired();

            #region Consent
            builder.Property(p => p.Consent)
               .HasColumnName("Consent")
               .IsRequired();

            builder.Property(p => p.By)
               .HasColumnName("By")
               .IsRequired();

            builder.Property(p => p.Type)
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();
            #endregion

            #endregion
        }
    }
}
