namespace Infrastructure.Mapping;

using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductMap : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("integer").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
        builder.Property(p => p.Name).HasColumnName("name").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(p => p.ImageUrl).HasColumnName("image_url").HasColumnType("nvarchar(100)").IsRequired(false);

        builder.Property(p => p.Created).HasColumnName("created").HasColumnType("datetime").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.LastModified).HasColumnName("last_modified").HasColumnType("datetime").IsRequired(false);
        builder.Property(p => p.LastModifiedBy).HasColumnName("last_modified_by").HasColumnType("nvarchar(100)").IsRequired(false);

        builder.HasOne(p => p.Restaurant).WithMany(p => p.Products).HasForeignKey("RestaurantId").OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.Promotions).WithOne(p => p.Product).HasForeignKey("ProductId").OnDelete(DeleteBehavior.Cascade);

        // builder.HasMany(p => p.Promotions).WithOne(p => p.Product).UsingEntity<Dictionary<string, object>>("ProductPromotion",
        //                                                   product => product.HasOne<PromotionEntity>()
        //                                                               .WithMany()
        //                                                               .HasForeignKey("ProductId")
        //                                                               .HasConstraintName("FK_ProductPromotion_ProductId")
        //                                                               .OnDelete(DeleteBehavior.Cascade),

        //                                                     promotion => promotion.HasOne<ProductEntity>()
        //                                                               .WithMany()
        //                                                               .HasForeignKey("PromotionId")
        //                                                               .HasConstraintName("FK_ProductPromotion_PromotionId")
        //                                                               .OnDelete(DeleteBehavior.Cascade));
    }
}

