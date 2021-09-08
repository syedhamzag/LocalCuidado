using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PersonalityTestMap : IEntityTypeConfiguration<PersonalityTest>
    {
        public void Configure(EntityTypeBuilder<PersonalityTest> builder)
        {
            builder.ToTable("tbl_PersonalityTest");
            builder.HasKey(k => k.TestId);

            #region Properties
            builder.Property(p => p.TestId)
               .HasColumnName("TestId")
               .IsRequired();

            builder.Property(p => p.GoalId)
               .HasColumnName("GoalId")
               .IsRequired();

            builder.Property(p => p.Question)
               .HasColumnName("Question")
               .IsRequired();

            builder.Property(p => p.Answer)
               .HasColumnName("Answer")
               .IsRequired();
            #endregion
        }
    }
}
