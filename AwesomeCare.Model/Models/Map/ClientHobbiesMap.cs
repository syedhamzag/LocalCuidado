using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientHobbiesMap : IEntityTypeConfiguration<ClientHobbies>
    {
        public void Configure(EntityTypeBuilder<ClientHobbies> builder)
        {
            builder.ToTable("tbl_ClientHobbies");
            builder.HasKey(k => k.CHId);

            #region Properties
            builder.Property(p => p.HId)
               .HasColumnName("HId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();
            #endregion
            #region Relation
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientHobbies)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
