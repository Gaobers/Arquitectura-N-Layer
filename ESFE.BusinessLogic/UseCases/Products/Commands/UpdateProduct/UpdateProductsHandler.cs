using ESFE.BusinessLogic.DTOs;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProducts
{
    internal sealed class UpdateProductsHandler(IEfRepository<Product> _repository)
        : IRequestHandler<UpdateProductsCommand, long>
    {
        public async Task<long> Handle(UpdateProductsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _repository.GetByIdAsync(command.Request.ProductId, cancellationToken);
                if (existingProduct == null) return 0;

                existingProduct = command.Request.Adapt(existingProduct);
                await _repository.UpdateAsync(existingProduct, cancellationToken);

                return existingProduct.ProductId;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
