using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFE.BusinessLogic.UseCases.Cotations.Queries.GetNewQuotationNumber;

public record GetNewQuotationNumberQuery : IRequest<long>;

