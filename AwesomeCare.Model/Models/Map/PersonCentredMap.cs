using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PersonCentredMap : IEntityTypeConfiguration<PersonCentred>
    {
        public void Configure(EntityTypeBuilder<PersonCentred> builder)
        {
            builder.ToTable("tbl_PersonCentred");
            builder.HasKey(k => k.PersonCentredId);

            #region Properties

            builder.Property(p => p.PersonCentredId)
               .HasColumnName("PersonCentredId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Class)
              .HasColumnName("Class")
              .IsRequired();

            builder.Property(p => p.ExpSupport)
             .HasColumnName("ExpSupport")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasMany<PersonCentredFocus>(p => p.Focus)
                .WithOne(p => p.PersonCentre)
                .HasForeignKey(p => p.PersonCentredId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
