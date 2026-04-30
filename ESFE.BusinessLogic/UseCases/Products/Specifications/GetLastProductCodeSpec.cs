using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Products.Specifications
{
    public class GetLastProductCodeSpec : Specification<product>
    {
        public GetLastProductCodeSpec(string prefix)
            Query.Where(PaginationEvaluator => p.ProductCode != null && p.ProductCode.StartWith(prefix))
                .OrderByDescending(p => p.ProductCode);
       }
    }
}
