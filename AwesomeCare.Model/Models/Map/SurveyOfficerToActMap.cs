using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SurveyOfficerToActMap : IEntityTypeConfiguration<SurveyOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<SurveyOfficerToAct> builder)
        {
            builder.ToTable("tbl_SurveyOfficerToAct");
            builder.HasKey(k => k.SurveyOfficerToActId);

            #region Properties
            builder.Property(p => p.SurveyOfficerToActId)
               .HasColumnName("SurveyOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.StaffSurveyId)
             .HasColumnName("StaffSurveyId")
             .IsRequired();

            #endregion
        }
    }
}
