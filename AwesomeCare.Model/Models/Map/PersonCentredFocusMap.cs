using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PersonCentredFocusMap : IEntityTypeConfiguration<PersonCentredFocus>
    {
        public void Configure(EntityTypeBuilder<PersonCentredFocus> builder)
        {
            builder.ToTable("tbl_PersonCentredFocus");
            builder.HasKey(k => k.PersonCentredFocusId);

            #region Properties

            builder.Property(p => p.PersonCentredFocusId)
               .HasColumnName("PersonCentredFocusId")
               .IsRequired();

            builder.Property(p => p.PersonCentredId)
               .HasColumnName("PersonCentredId")
               .IsRequired();

            #endregion
        }
    }
}
