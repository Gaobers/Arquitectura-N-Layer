using ESFE.BusinessLogic.DTOs;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts
{
    internal sealed class GetProductsHandler(IEfRepository<Products> _repository) : IRequestHandler<GetProductsQuery, List<ProductResponse>>
    {
        public  asyncTask<List<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {

        }
    }
}
