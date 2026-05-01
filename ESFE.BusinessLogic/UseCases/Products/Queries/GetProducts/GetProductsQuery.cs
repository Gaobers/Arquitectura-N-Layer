using ESFE.BusinessLogic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts;

public record GetProductsQuery(long ProductId) : IRequest<ProductByIdResponse>;
