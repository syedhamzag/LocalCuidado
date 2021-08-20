using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HealthAndLivingMap : IEntityTypeConfiguration<HealthAndLiving>
    {
        public void Configure(EntityTypeBuilder<HealthAndLiving> builder)
        {
            builder.ToTable("tbl_HealthAndLiving");
            builder.HasKey(k => k.HLId);

            #region Properties
            builder.Property(p => p.HLId)
               .HasColumnName("HLId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.AbilityToRead)
               .HasColumnName("AbilityToRead")
               .IsRequired();

            builder.Property(p => p.AlcoholicDrink)
               .HasColumnName("AlcoholicDrink")
               .IsRequired();

            builder.Property(p => p.AllowChats)
               .HasColumnName("AllowChats")
               .IsRequired();

            builder.Property(p => p.BriefHealth)
               .HasColumnName("BriefHealth")
               .IsRequired();

            builder.Property(p => p.CareSupport)
               .HasColumnName("CareSupport")
               .IsRequired();

            builder.Property(p => p.ConstraintDetails)
               .HasColumnName("ConstraintDetails")
               .IsRequired();

            builder.Property(p => p.ConstraintRequired)
               .HasColumnName("ConstraintRequired")
               .IsRequired();

            builder.Property(p => p.ContinenceIssue)
               .HasColumnName("ContinenceIssue")
               .IsRequired();

            builder.Property(p => p.ContinenceNeeds)
               .HasColumnName("ContinenceNeeds")
               .IsRequired();

            builder.Property(p => p.ContinenceSource)
               .HasColumnName("ContinenceSource")
               .IsRequired();

            builder.Property(p => p.DehydrationRisk)
               .HasColumnName("DehydrationRisk")
               .IsRequired();

            builder.Property(p => p.EatingWithStaff)
               .HasColumnName("EatingWithStaff")
               .IsRequired();

            builder.Property(p => p.Email)
               .HasColumnName("Email")
               .IsRequired();

            builder.Property(p => p.FamilyUpdate)
               .HasColumnName("FamilyUpdate")
               .IsRequired();

            builder.Property(p => p.FinanceManagement)
               .HasColumnName("FinanceManagement")
               .IsRequired();

            builder.Property(p => p.LaundaryRequired)
               .HasColumnName("LaundaryRequired")
               .IsRequired();

            builder.Property(p => p.LetterOpening)
               .HasColumnName("LetterOpening")
               .IsRequired();

            builder.Property(p => p.LifeStyle)
               .HasColumnName("LifeStyle")
               .IsRequired();

            builder.Property(p => p.MeansOfComm)
               .HasColumnName("MeansOfComm")
               .IsRequired();

            builder.Property(p => p.MovingAndHandling)
               .HasColumnName("MovingAndHandling")
               .IsRequired();

            builder.Property(p => p.NeighbourInvolment)
               .HasColumnName("NeighbourInvolment")
               .IsRequired();

            builder.Property(p => p.ObserveHealth)
               .HasColumnName("ObserveHealth")
               .IsRequired();

            builder.Property(p => p.PostalService)
               .HasColumnName("PostalService")
               .IsRequired();

            builder.Property(p => p.PressureSore)
               .HasColumnName("PressureSore")
               .IsRequired();

            builder.Property(p => p.ShoppingRequired)
               .HasColumnName("ShoppingRequired")
               .IsRequired();

            builder.Property(p => p.Smoking)
               .HasColumnName("Smoking")
               .IsRequired();

            builder.Property(p => p.SpecialCaution)
               .HasColumnName("SpecialCaution")
               .IsRequired();

            builder.Property(p => p.SpecialCleaning)
               .HasColumnName("SpecialCleaning")
               .IsRequired();

            builder.Property(p => p.SupportToBed)
               .HasColumnName("SupportToBed")
               .IsRequired();

            builder.Property(p => p.TVandMusic)
              .HasColumnName("TVandMusic")
              .IsRequired();

            builder.Property(p => p.TeaChocolateCoffee)
               .HasColumnName("TeaChocolateCoffee")
               .IsRequired();

            builder.Property(p => p.TextFontSize)
               .HasColumnName("TextFontSize")
               .IsRequired();

            builder.Property(p => p.VideoCallRequired)
               .HasColumnName("VideoCallRequired")
               .IsRequired();

            builder.Property(p => p.WakeUp)
               .HasColumnName("WakeUp")
               .IsRequired();

            builder.Property(p => p.ConstraintAttachment)
               .HasColumnName("ConstraintAttachment")
               .IsRequired();
            #endregion
        }
    }
}
