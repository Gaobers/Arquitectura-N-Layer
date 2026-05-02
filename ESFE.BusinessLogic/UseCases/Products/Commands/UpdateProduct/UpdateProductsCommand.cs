using ESFE.BusinessLogic.DTOs;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.UpdateProducts
{
    public record UpdateProductsCommand(UpdateProductRequest Request) : IRequest<long>;
}
