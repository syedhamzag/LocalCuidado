using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffTrainingMatrixMap : IEntityTypeConfiguration<StaffTrainingMatrix>
    {
        public void Configure(EntityTypeBuilder<StaffTrainingMatrix> builder)
        {
            builder.ToTable("tbl_StaffTrainingMatrix");
            builder.HasKey(k => k.MatrixId);

            #region Properties
            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffPersonalInfo)
                 .WithMany(p => p.StaffTrainingMatrix)
                 .HasForeignKey(p => p.StaffPersonalInfoId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<StaffTrainingMatrixList>(p => p.StaffTrainingMatrixList)
                .WithOne(p => p.StaffTrainingMatrix)
                .HasForeignKey(p => p.MatrixId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
