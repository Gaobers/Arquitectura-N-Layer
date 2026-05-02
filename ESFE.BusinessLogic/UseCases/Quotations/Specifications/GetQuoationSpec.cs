using Ardalis.Specification;
using ESFE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Quotations.Specifications
{
    public class GetQuoationSpec : Specification<Quotation>
    {
        public GetQuoationSpec()  {
            Query.Include(q=>q .QuotationDetails);
            Query.Include(q=>q.User);
        }
    }

}
