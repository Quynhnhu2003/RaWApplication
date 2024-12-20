﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class MediumConfiguration : IEntityTypeConfiguration<Medium>
    {
        public MediumConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<Medium> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.FileName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Extension)
                .IsRequired()
                .HasMaxLength(7);

        }
    }
}
