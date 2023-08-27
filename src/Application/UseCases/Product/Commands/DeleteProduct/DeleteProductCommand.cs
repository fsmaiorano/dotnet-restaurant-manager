using Application.Common.Interfaces;
using Domain.Events.Product;
using MediatR;

namespace Application.UseCases.Product.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IDataContext _context;

    public DeleteProductCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(request.Id) ??
                            throw new Exception("Product not found");

        _context.Products.Remove(entity);

        entity.AddDomainEvent(new ProductDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
