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
    public class BlogPostsConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Title)
                .HasMaxLength(255);
            builder.Property(p => p.ShortDescription)
                .HasMaxLength(255);
            builder.Property(p => p.Content)
                .HasMaxLength(500);
            builder.Property(p => p.FeaturedImageUrl)
                .HasMaxLength(255);
            builder.Property(p => p.UrlHandle)
                .HasMaxLength(255);
            builder.Property(p => p.PublishedDate)
                .IsRequired();
            builder.Property(p => p.Author)
                .HasMaxLength(100);
        
        }
    }
}
