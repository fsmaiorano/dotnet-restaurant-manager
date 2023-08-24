namespace Infrastructure.Mapping;

using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PromotiontMap : IEntityTypeConfiguration<PromotionEntity>
{
    public void Configure(EntityTypeBuilder<PromotionEntity> builder)
    {
        builder.ToTable("Promotion");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id").HasColumnType("integer").IsRequired().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
        builder.Property(p => p.Description).HasColumnName("description").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.PromotionalPrice).HasColumnName("promotional_price").HasColumnType("decimal(18,2)").IsRequired();
        // builder.Property(p => p.DaysAndTimes).HasColumnName("days_and_times").HasColumnType("nvarchar(100)").IsRequired();

        builder.Property(p => p.Created).HasColumnName("created").HasColumnType("datetime").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(p => p.LastModified).HasColumnName("last_modified").HasColumnType("datetime").IsRequired(false);
        builder.Property(p => p.LastModifiedBy).HasColumnName("last_modified_by").HasColumnType("nvarchar(100)").IsRequired(false);
    }
}

