using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class KeyWorkerOfficerToActMap : IEntityTypeConfiguration<KeyWorkerOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<KeyWorkerOfficerToAct> builder)
        {
            builder.ToTable("tbl_KeyWorkerOfficerToAct");
            builder.HasKey(k => k.KeyWorkerOfficerToActId);

            #region Properties
            builder.Property(p => p.KeyWorkerOfficerToActId)
               .HasColumnName("KeyWorkerOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.KeyWorkerId)
             .HasColumnName("KeyWorkerId")
             .IsRequired();

            #endregion
        }
    }
}
