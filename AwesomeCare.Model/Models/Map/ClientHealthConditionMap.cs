using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientHealthConditionMap : IEntityTypeConfiguration<ClientHealthCondition>
    {
        public void Configure(EntityTypeBuilder<ClientHealthCondition> builder)
        {
            builder.ToTable("tbl_ClientHealthCondition");
            builder.HasKey(k => k.CHCId);

            #region Properties
            builder.Property(p => p.HCId)
               .HasColumnName("HCId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();
            #endregion
            #region Relation
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientHealthCondition)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
