using Application.Dtos.Report;
using MediatR;

namespace Application.Queries.orderQueries.MostSoldProduct;

public class GetMostSoldProductsQuery: IRequest<IEnumerable<MostSoldProductDto>>
{
    public int TopN { get; set; } = 10; 
}