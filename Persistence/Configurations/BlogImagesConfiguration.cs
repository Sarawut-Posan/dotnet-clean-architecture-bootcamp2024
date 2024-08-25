using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class BlogImagesConfiguration : IEntityTypeConfiguration<BlogImage>
    {
        public void Configure(EntityTypeBuilder<BlogImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.FileName)
                .HasMaxLength(255);
            builder.Property(p => p.FileExtension)
                .HasMaxLength(100);
            builder.Property(p => p.Title)
                .HasMaxLength(100);
            builder.Property(p => p.Url)
                .HasMaxLength(255);
            builder.Property(p => p.DateCreated)
                .IsRequired();
        }
    }
}
