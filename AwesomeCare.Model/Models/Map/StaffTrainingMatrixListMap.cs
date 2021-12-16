using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    class StaffTrainingMatrixListMap : IEntityTypeConfiguration<StaffTrainingMatrixList>
    {
        public void Configure(EntityTypeBuilder<StaffTrainingMatrixList> builder)
        {
            builder.ToTable("tbl_StaffTrainingMatrixList");
            builder.HasKey(p => p.TrainingId);

            #region Properties
            builder.Property(p => p.MatrixId)
                .HasColumnName("MatrixId")
                .IsRequired();

            builder.Property(p => p.Date)
                .HasColumnName("Date")
                .IsRequired();
            #endregion
        }
    }
}
