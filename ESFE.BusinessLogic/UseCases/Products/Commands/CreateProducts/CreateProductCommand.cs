using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Products.Commands.CreateProduct;

public record CreateProductCommand(CreateProductRequest Request) : IRequest<long>;
{
}
