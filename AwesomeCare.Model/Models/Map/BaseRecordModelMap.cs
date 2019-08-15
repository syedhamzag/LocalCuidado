using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BaseRecordModelMap : IEntityTypeConfiguration<BaseRecordModel>
    {
        public void Configure(EntityTypeBuilder<BaseRecordModel> builder)
        {
            builder.ToTable("tbl_BaseRecord");
            builder.HasKey(p => p.BaseRecordId);

            #region Properties
            
            builder.Property(p => p.BaseRecordId)
                .HasColumnName("BaseRecordId")
                .IsRequired();

            builder.Property(p => p.KeyName)
                .HasColumnName("KeyName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasMany<BaseRecordItemModel>(p => p.BaseRecordItems)
                .WithOne(p => p.BaseRecord)
                .HasForeignKey(p => p.BaseRecordId)
                .IsRequired();
            #endregion
        }
    }
}
