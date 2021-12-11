using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CareIssuesTaskMap : IEntityTypeConfiguration<CareIssuesTask>
    {
        public void Configure(EntityTypeBuilder<CareIssuesTask> builder)
        {
            builder.ToTable("tbl_CareIssuesTask");
            builder.HasKey(k => k.CareIssuesTaskId);

            #region Properties

            builder.Property(p => p.BestId)
               .HasColumnName("BestId")
               .IsRequired();

            builder.Property(p => p.Issues)
                .HasColumnName("Issues")
                .IsRequired();

            #endregion
        }
    }
}
