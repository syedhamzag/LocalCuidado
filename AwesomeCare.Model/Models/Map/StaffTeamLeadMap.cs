using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffTeamLeadMap : IEntityTypeConfiguration<StaffTeamLead>
    {
        public void Configure(EntityTypeBuilder<StaffTeamLead> builder)
        {
            builder.ToTable("tbl_StaffTeamLead");
            builder.HasKey(p => p.TeamLeadId);

            #region Properties
            builder.Property(p => p.Date)
                .HasColumnName("Date")
                .IsRequired();

            builder.Property(p => p.Rota)
                .HasColumnName("Rota")
                .IsRequired();

            builder.Property(p => p.ClientInvolved)
                .HasColumnName("ClientInvolved")
                .IsRequired();

            builder.Property(p => p.StaffInvolved)
                .HasColumnName("StaffInvolved")
                .IsRequired();

            builder.Property(p => p.DidYouObserved)
                .HasColumnName("DidYouObserved")
                .IsRequired();

            builder.Property(p => p.DidYouDo)
                .HasColumnName("DidYouDo")
                .IsRequired();

            builder.Property(p => p.OfficeToDo)
                .HasColumnName("OfficeToDo")
                .IsRequired();

            builder.Property(p => p.StaffStoppedWorking)
                .HasColumnName("StaffStoppedWorking")
                .IsRequired();


            #endregion

            #region RelationShip

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.StaffTeamLead)
                 .HasForeignKey(p => p.ClientInvolved)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.StaffPersonalInfo)
                 .WithMany(p => p.StaffTeamLead)
                 .HasForeignKey(p => p.StaffInvolved)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<StaffTeamLeadTasks>(p => p.StaffTeamLeadTasks)
                .WithOne(p => p.StaffTeamLead)
                .HasForeignKey(p => p.TeamLeadId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
