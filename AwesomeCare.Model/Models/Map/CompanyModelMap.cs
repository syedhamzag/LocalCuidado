using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CompanyModelMap : IEntityTypeConfiguration<CompanyModel>
    {
        public void Configure(EntityTypeBuilder<CompanyModel> builder)
        {
           
            builder.ToTable("tbl_Company");
            builder.HasKey(p => p.CompanyId);

            #region Properties
           
            builder.Property(p => p.Address)
                 .HasColumnName("Address")
                 .HasMaxLength(255)
                 .IsRequired();

            builder.Property(p => p.CompanyId)
                .HasColumnName("CompanyId")
                .IsRequired();

            builder.Property(p => p.CompanyName)
                .HasColumnName("CompanyName")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.LogoUrl)
               .HasColumnName("LogoUrl");

            builder.Property(p => p.Email)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Website)
               .HasColumnName("Website")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Language)
              .HasColumnName("Language")
              .HasMaxLength(255)
              .IsRequired();

           
            #endregion
        }
    }
}
