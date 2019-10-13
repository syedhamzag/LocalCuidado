using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientInvolvingPartyMap : IEntityTypeConfiguration<ClientInvolvingParty>
    {
        public void Configure(EntityTypeBuilder<ClientInvolvingParty> builder)
        {
            builder.ToTable("tbl_ClientInvolvingParty");
            builder.HasKey(p => p.ClientInvolvingPartyId);

            #region Properties

            //     public int  { get; set; }
            //public int  { get; set; }
            //public int  { get; set; }
            //public string  { get; set; }
            //public string  { get; set; }
            //public string  { get; set; }
            //public string  { get; set; }
            //public string  { get; set; }


            builder.Property(p => p.ClientInvolvingPartyId)
                .HasColumnName("ClientInvolvingPartyId")
                .IsRequired();

            builder.Property(p => p.ClientInvolvingPartyItemId)
                .HasColumnName("ClientInvolvingPartyItemId")
                .IsRequired();

            builder.Property(p => p.ClientId)
                 .HasColumnName("ClientId")
                 .IsRequired();

            builder.Property(p => p.Name)
                 .HasColumnName("Name")
                 .HasMaxLength(50)
                 .IsRequired();

            builder.Property(p => p.Address)
                .HasColumnName("Address")
                .HasMaxLength(225)
                .IsRequired();

            builder.Property(p => p.Email)
               .HasColumnName("Email")
               .HasMaxLength(125)
               .IsRequired();

            builder.Property(p => p.Telephone)
               .HasColumnName("Telephone")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Relationship)
              .HasColumnName("Relationship")
              .HasMaxLength(50)
              .IsRequired();

            builder.HasOne<Client>(p => p.Client)
                .WithMany(c => c.InvolvingParties)
                .HasForeignKey(k => k.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<ClientInvolvingPartyItem>(p => p.ClientInvolvingPartyItem)
                .WithMany(m => m.ClientInvolvingParty)
                .HasForeignKey(k => k.ClientInvolvingPartyItemId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
