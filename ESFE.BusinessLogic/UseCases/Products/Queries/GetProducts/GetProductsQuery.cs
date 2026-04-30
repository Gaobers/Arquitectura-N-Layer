using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Products.Queries.GetProducts

public record GetProductsQuery : IRequest<List<ProductResponse>>;
