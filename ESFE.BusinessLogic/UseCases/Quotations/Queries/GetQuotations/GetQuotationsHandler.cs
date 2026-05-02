using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Cotations.Queries.GetQuotations;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Cotations.Queries.GetQuotations;

internal sealed class GetQuotationsHandler(IEfRepository<Quotation> _Repository)
    : IRequestHandler<GetQuotationsQuery, List<QuotationResponse>>
{
    public async Task<List<QuotationResponse>> Handle(GetQuotationsQuery request, CancellationToken cancellationToken)
    {
        var quotations = await _Repository.ListAsync(new GetQuotationsSpec(),cancellationToken);

        if (quotations == null || !quotations.Any())
        {

            return new List<QuotationResponse>();
        }
        return quotations.Adapt<List<QuotationResponse>>();
       
       
    }
}
