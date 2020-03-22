using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffWorkTeamMap : IEntityTypeConfiguration<StaffWorkTeam>
    {
        public void Configure(EntityTypeBuilder<StaffWorkTeam> builder)
        {
            builder.ToTable("tbl_StaffWorkTeam");
            builder.HasKey(p => p.StaffWorkTeamId);

            #region Properties
            builder.Property(p => p.StaffWorkTeamId)
                .HasColumnName("StaffWorkTeamId")
                .IsRequired();

            builder.Property(p => p.WorkTeam)
                .HasColumnName("WorkTeam")
                .IsRequired();
            #endregion
        }
    }
}
