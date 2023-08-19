using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IDataContext
{
    DbSet<RestaurantEntity> Restaurants { get; }
    DbSet<PromotionEntity> Promotions { get; }
    DbSet<ProductEntity> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
