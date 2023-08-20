namespace Infrastructure.Mapping;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RestaurantMap : IEntityTypeConfiguration<RestaurantEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantEntity> builder)
    {
        builder.ToTable("Restaurant");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("int").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasColumnType("nvarchar(250)").IsRequired();
        builder.Property(p => p.Address).HasColumnName("address").HasColumnType("nvarchar(250)").IsRequired();
        builder.Property(p => p.ImageUrl).HasColumnName("image_url").HasColumnType("nvarchar(250)").IsRequired(false);

        builder.Property(p => p.Created).HasColumnName("created").HasColumnType("datetime").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.LastModified).HasColumnName("last_modified").HasColumnType("datetime").IsRequired(false);
        builder.Property(p => p.LastModifiedBy).HasColumnName("last_modified_by").HasColumnType("nvarchar(100)").IsRequired(false);
    }
}

