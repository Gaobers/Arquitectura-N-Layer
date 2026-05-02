using MediatR;

namespace ESFE.BusinessLogic.UseCases.Quotations.Queries.GetNewQuotationNumber;

public record GetNewQuotationNumberQuery : IRequest<long>;

