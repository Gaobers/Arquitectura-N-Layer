using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Products.Specifications
{
    public class GetProductWithBrandSpec : Specifications<Products>
    {
        public GetProductWithBrandSpec() { 
            Query.Include(p=> p.Brand);
            }
    }
}
