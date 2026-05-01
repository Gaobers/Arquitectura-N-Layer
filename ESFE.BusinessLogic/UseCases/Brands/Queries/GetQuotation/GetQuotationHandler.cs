using ESFE.BusinessLogic.DTOs;
using ESFE.DataAccess.Interfaces;
using ESFE.Entities;
using Mapster;
using MediatR;

namespace ESFE.BusinessLogic.UseCases.Brands.Queries.GetQuotation;

internal sealed class GetQuotationHandler(IEfRepository<Quotation> _repository)
: IRequestHandler<GetQuotationQuery, QuotationResponse>
{
    public async Task<QuotationResponse> Handle(GetQuotationQuery query, CancellationToken cancellationToken)
    {
        var quotation = await _repository.GetByIdAsync(query.QuotationId, cancellationToken);

        if (quotation is null)
        {
            return new QuotationResponse();
        }

        return quotation.Adapt<QuotationResponse>();
    }
}


