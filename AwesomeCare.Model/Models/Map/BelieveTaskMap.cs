using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BelieveTaskMap : IEntityTypeConfiguration<BelieveTask>
    {
        public void Configure(EntityTypeBuilder<BelieveTask> builder)
        {
            builder.ToTable("tbl_BelieveTask");
            builder.HasKey(k => k.BelieveTaskId);

            #region Properties

            builder.Property(p => p.BestId)
               .HasColumnName("BestId")
               .IsRequired();

            builder.Property(p => p.ReasonableBelieve)
                .HasColumnName("ReasonableBelieve")
                .IsRequired();


            #endregion
        }

    }
}
