using Application.Dtos.Product;
using MediatR;

namespace Application.Queries.productQueries.GetSingleProduct;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
