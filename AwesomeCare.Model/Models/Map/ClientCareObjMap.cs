using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientCareObjMap : IEntityTypeConfiguration<ClientCareObj>
    {
        public void Configure(EntityTypeBuilder<ClientCareObj> builder)
        {
            builder.ToTable("tbl_ClientCareObj");
            builder.HasKey(k => k.CareObjId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.Note)
               .HasColumnName("Note")
               .IsRequired();

            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientCareObj)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<CareObjPersonToAct>(p => p.PersonToAct)
                .WithOne(p => p.ClientCareObj)
                .HasForeignKey(p => p.CareObjId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
