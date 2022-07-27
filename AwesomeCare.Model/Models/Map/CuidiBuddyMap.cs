using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CuidiBuddyMap : IEntityTypeConfiguration<CuidiBuddy>
    {
        public void Configure(EntityTypeBuilder<CuidiBuddy> builder)
        {
            builder.ToTable("tbl_CuidiBuddy");
            builder.HasKey(k => k.Id);

            #region Properties
            builder.Property(p => p.CuidiBuddyId)
               .HasColumnName("CuidiBuddyId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();
            #endregion

            #region Relation
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.CuidiBuddy)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
