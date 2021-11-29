using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientDailyTaskMap : IEntityTypeConfiguration<ClientDailyTask>
    {
        public void Configure(EntityTypeBuilder<ClientDailyTask> builder)
        {
            builder.ToTable("tbl_ClientDailyTask");
            builder.HasKey(k => k.DailyTaskId);

            #region Properties

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.DailyTaskName)
               .HasColumnName("DailyTaskName")
               .IsRequired();

            builder.Property(p => p.Date)
              .HasColumnName("Date")
              .IsRequired();

            builder.Property(p => p.AmendmentDate)
             .HasColumnName("AmendmentDate")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientDailyTask)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
