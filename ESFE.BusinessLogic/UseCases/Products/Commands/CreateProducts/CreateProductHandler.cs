using ESFE.BusinessLogic.DTOs;
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
                var words = command.Request.ProductCode.Split(' ');
                var prefix = string.Concat(words[0][0], words.Length > 1 ? words[1][0] : 'X').ToUpper();
                var lastProduct = await _repository.FirstOrDefaultAsync(new GetLastProductCodeSpec(prefix));

                int newNumber = 1;

                if (lastProduct != null && string.IsNullOrEmpty(lastProduct.ProductCode))
                {
                    var numberParte = lastProduct.ProductCode.Substring(prefix.Length);
                    if (int.TryParse(numberParte, out int lastNumber))
                    {
                        newNumber = lastNumber + 1;
                    }
                }

                command.Request.ProductCode = $"{prefix}{newNumber}:D4";

                var newProduct = command.Request.Adapt<Product>();
                var createdProduct = await _repository.AddAsync(newProduct, cancellationToken);
                return createdProduct.ProductId;
               
            }
            catch (Exception)
            {
                return 0;
                throw;
            }

        }

    }
}
