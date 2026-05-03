using ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;
using ESFE.BusinessLogic.UseCases.Products.Specifications;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.CreateProducts
{
    internal sealed class CreateProductHandler(IEfRepository<Product> _repository)
        : IRequestHandler<CreateProductCommand, long>
    {
        public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var newProduct = command.Request.Adapt<Product>();

                var createdProduct = await _repository.AddAsync(newProduct, cancellationToken);

                return createdProduct.ProductId;
            }
            catch
            {
                throw;
            }
        }

    }
}
